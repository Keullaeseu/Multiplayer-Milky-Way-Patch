using System.Reflection;
using HarmonyLib;
using Multiplayer.Compat;
using Verse;

namespace MultiplayerMilkyWayPatch.Source.Mods;

/// <summary>
///     Multiplayer Patch for Milky Way by Andromeda,
///     Last Update: 30 Aug @ 4:21pm 2026
///     <see href="https://steamcommunity.com/sharedfiles/filedetails/?id=3773448562" />
///     MilkyWay is a UI-only framework (widgets, layouts, render helpers, text effects).
///     It holds no game state and exposes no gizmos or syncable actions, so there is
///     nothing to register with <c>MP.RegisterSyncMethod</c>.
///     The only desync vector is <c>MilkyWay.TextUtils</c> (and its nested
///     <c>ObfuscatedTextTransformer</c>), which uses <c>Verse.Rand</c> for purely
///     visual text-obfuscation effects. Those calls happen during GUI rendering at
///     different times on each client, so they must not consume shared synced RNG.
///     We isolate them with <c>Rand.PushState/PopState</c> via
///     <see cref="PatchingUtilities.PatchPushPopRand(IEnumerable{MethodBase})" />.
/// </summary>
[MpCompatFor("Andromeda.MilkyWay")]
public class MilkyWay
{
    private const string LogPrefix = "[Multiplayer Milky Way Patch]";

    public MilkyWay(ModContentPack content)
    {
        Log.Message($"{LogPrefix} Initializing...");

        PatchTextUtilsRng();

        Log.Message($"{LogPrefix} Initialized.");
    }

    private static void PatchTextUtilsRng()
    {
        var textUtilsType = AccessTools.TypeByName("MilkyWay.TextUtils");
        if (textUtilsType == null)
        {
            Log.Error($"{LogPrefix} Could not find type MilkyWay.TextUtils, RNG patch skipped.");
            return;
        }

        var transformerType = AccessTools.Inner(textUtilsType, "ObfuscatedTextTransformer");
        if (transformerType == null)
            Log.Error(
                $"{LogPrefix} Could not find type MilkyWay.TextUtils+ObfuscatedTextTransformer, nested RNG patch skipped.");

        var methods = new List<MethodBase>
        {
            AccessTools.DeclaredMethod(textUtilsType, "RandomWordLength"),
            AccessTools.DeclaredMethod(textUtilsType, "GenerateSpaceMask"),
            AccessTools.DeclaredMethod(textUtilsType, "ObfuscateFakeWords"),
            AccessTools.DeclaredMethod(textUtilsType, "Obfuscate"),
            AccessTools.DeclaredMethod(textUtilsType, "ObfuscateWithMask")
        };

        if (transformerType != null)
        {
            methods.Add(AccessTools.DeclaredMethod(transformerType, "Obfuscate"));
            methods.Add(AccessTools.DeclaredMethod(transformerType, "RandomDelay"));
        }

        var resolved = new List<MethodBase>();
        foreach (var method in methods)
        {
            if (method == null)
            {
                Log.Warning(
                    $"{LogPrefix} One of the TextUtils RNG methods was not found, skipping it. Was the method removed or renamed?");
                continue;
            }

            resolved.Add(method);
        }

        if (resolved.Count == 0)
        {
            Log.Error($"{LogPrefix} No TextUtils RNG methods resolved, nothing patched.");
            return;
        }

        PatchingUtilities.PatchPushPopRand(resolved);
        Log.Message($"{LogPrefix} Patched {resolved.Count} TextUtils RNG methods with Push/Pop.");
    }
}