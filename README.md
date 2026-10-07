# Mossela

2D mobile game (iOS and Android) built with Unity 6.3 LTS and URP 2D.

## Open the project
1. Install Unity 6.3 LTS (6000.3.x) with iOS and Android build support.
2. Install Git LFS (`brew install git-lfs && git lfs install`) before cloning.
3. Open the folder in Unity Hub. On first load, `Assets/Editor/UrpProjectSetup.cs` creates the URP 2D assets in `Assets/Settings`. Commit them.
4. Open `Assets/Scenes/Boot/Boot.unity` and press Play. Boot loads `Cottage`.

## Settings
- Portrait, 1080x1920 reference. Bundle ID `com.mossela.game`.
- Sorting layers: Background, Furniture, Character, Foreground, Overlay.
- Sprites import at 256 pixels per unit. A 1254 px Haru sprite is about 4.9 units wide.

## Haru
- Prefab: `Assets/Prefabs/Characters/Haru.prefab`.
- `HaruController` is the public API (`SetPose`, `SetExpression`, `SetState`).
- Poses and expressions are ScriptableObjects in `Assets/ScriptableObjects`.
- `HaruVisual` draws either a full-character sprite (default) or the modular rig (`Visual/Modular`, disabled by default).
- `MicroAnimator` handles breathing, blink, ear twitch and tail sway.
- The modular parts do not share one canvas, apart from the head group. Align them in the editor before enabling modular mode, and set pivots on the ears and tail.

## Cottage scene layout
- `Environment`: `Wall`, `Baseboard`, `Floor`, `Window` (`Frame`, `Glass`, `Mullion_V`, `Mullion_H`, `Sill`). Sorting layer Background.
- `Furniture`: `ChairSpot` (holds the Chair prefab), `Zone_Shelf`, `Zone_Bed`, `Zone_Table`. Sorting layer Furniture.
- `Character`: Haru. Sorting layer Character.
- `Systems`: `Main Camera` (orthographic size 6.4, 7.2 x 12.8 units in portrait) and `Interaction`.
- Every placeholder is a tinted, scaled `Assets/Art/Environment/Placeholders/Placeholder_Square.png`. To swap in final art, assign the new sprite to that object, set its scale back to 1 and tint to white.
