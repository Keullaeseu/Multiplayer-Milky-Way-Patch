# Multiplayer Milky Way Patch

A RimWorld Multiplayer compatibility patch for [Milky Way](https://steamcommunity.com/sharedfiles/filedetails/?id=3773448562).

This mod is designed to improve multiplayer synchronization when playing with the [Milky Way](https://steamcommunity.com/sharedfiles/filedetails/?id=3773448562) mod and RimWorld Multiplayer.

## Features

- Adds multiplayer compatibility for [Milky Way](https://steamcommunity.com/sharedfiles/filedetails/?id=3773448562).

## Requirements

- RimWorld
- [Harmony](https://steamcommunity.com/sharedfiles/filedetails/?id=2009463077)
- RimWorld Multiplayer
  - [GitHub version](https://github.com/rwmt/Multiplayer) or [Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=2606448745) version
- [Milky Way](https://steamcommunity.com/sharedfiles/filedetails/?id=3773448562)

The host and every connected player must use compatible versions of all required mods.

## Installation

### Steam Workshop

Subscribe to the required mods and add them to your RimWorld mod list in the following order:

1. Harmony
2. Core
3. Royalty, Ideology, Biotech, and Anomaly, if applicable
4. RimWorld Multiplayer
5. [Milky Way](https://steamcommunity.com/sharedfiles/filedetails/?id=3773448562)
6. [Multiplayer Milky Way Patch](https://github.com/Keullaeseu/Multiplayer-Milky-Way-Patch/releases/latest)

The patch should load after RimWorld Multiplayer and [Milky Way](https://steamcommunity.com/sharedfiles/filedetails/?id=3773448562).

### Manual Installation

1. Download the latest release from the [**Releases**](https://github.com/Keullaeseu/Multiplayer-Milky-Way-Patch/releases/latest) section.
2. Extract the mod folder into your RimWorld `Mods` directory.
3. Enable the required mods in RimWorld.
4. Use the recommended load order listed above.
5. Make sure every multiplayer player has the same mod list, configuration, and load order.

## Multiplayer Usage

All players should have the following mods installed and enabled:

- RimWorld Multiplayer
- [Milky Way](https://steamcommunity.com/sharedfiles/filedetails/?id=3773448562)
- [Multiplayer Milky Way Patch](https://github.com/Keullaeseu/Multiplayer-Milky-Way-Patch/releases/latest)
- All other required Milky Way dependencies

The host and all connected clients should use the same:

- RimWorld version
- RimWorld Multiplayer version
- Milky Way version
- Multiplayer Milky Way Patch version
- Mod configuration
- Mod load order

Do not add, remove, update, or reorder mods while players are connected to the same multiplayer session.

## Compatibility

This patch is intended to provide multiplayer compatibility for [Milky Way](https://steamcommunity.com/sharedfiles/filedetails/?id=3773448562)
itself. Milky Way is a UI-only framework (widgets, layouts, render helpers, text
effects) with no game state or gizmos, so there are no sync methods to register.
The patch isolates the visual text-obfuscation RNG in `MilkyWay.TextUtils`
(`RandomWordLength`, `GenerateSpaceMask`, `Obfuscate`, `ObfuscateWithMask`,
`ObfuscateFakeWords`, plus `ObfuscatedTextTransformer.Obfuscate` and
`RandomDelay`) with `Rand.PushState`/`PopState` so GUI rendering never consumes
shared synced RNG.

It does not replace:

- [RimWorld Multiplayer](https://steamcommunity.com/sharedfiles/filedetails/?id=2606448745)
- [Milky Way](https://steamcommunity.com/sharedfiles/filedetails/?id=3773448562)

## Known Limitations

- Compatibility may be affected by future RimWorld updates.
- Compatibility may be affected by future updates to RimWorld Multiplayer or Milky Way.

## Credits

- [RimWorld Multiplayer on GitHub](https://github.com/rwmt/Multiplayer)
- [RimWorld Multiplayer on Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=2606448745)
- [Milky Way on Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=3773448562)
- [Multiplayer Milky Way Patch](https://github.com/Keullaeseu)
