# Unity Essentials

This module is part of the Unity Essentials ecosystem and follows the same lightweight, editor-first approach.
Unity Essentials is a lightweight, modular set of editor utilities and helpers that streamline Unity development. It focuses on clean, dependency-free tools that work well together.

All utilities are under the `UnityEssentials` namespace.

```csharp
using UnityEssentials;
```

## Installation

Install the Unity Essentials entry package via Unity's Package Manager, then install modules from the Tools menu.

- Add the entry package (via Git URL)
    - Window → Package Manager
    - "+" → "Add package from git URL…"
    - Paste: `https://github.com/CanTalat-Yakan/UnityEssentials.git`

- Install or update Unity Essentials packages
    - Tools → Install & Update UnityEssentials
    - Install all or select individual modules; run again anytime to update

---

# UI Toolkit Animation

> Quick overview: Drop‑in USS utility classes for UI Toolkit transitions. Add a TransitionBase (duration/ease) and toggle state classes (opacity/translate/scale/rotate) to animate UI in and out - no custom code or keyframes required.

This package provides a ready‑made USS stylesheet with common animation utilities for UI Toolkit. Apply a base transition preset to a VisualElement, then add/remove classes like `TranslateLeft`, `Opacity0`, or `Scale1_1` to animate between states using UI Toolkit’s built‑in `transition-*` properties.

![screenshot](Documentation/Screenshot.png)

## Features
- Utility classes for common transforms
  - Opacity: `Opacity0`, `Opacity1`
  - Translate: `TranslateLeft`, `TranslateRight`, `TranslateUp`, `TranslateDown` (±100 px) and large `*X` variants (±1500 px)
  - Scale: `Scale0`, `Scale0_5`, `Scale0_9`, `Scale1`, `Scale1_01`, `Scale1_1`, `Scale1_5`, `Scale2`
  - Rotate: `Rotate0`, `Rotate45`, `Rotate90`, `RotateMinus45`, `RotateMinus90`, `Rotate180`
- Transition presets (ease‑in‑out on opacity/translate/scale/rotate)
  - `TransitionBase0` (0.05s), `TransitionBase1` (0.1s), `TransitionBase2` (0.25s), `TransitionBase3` (0.5s), `TransitionBase4` (0.75s), `TransitionBase5` (1s), `TransitionBase6` (1.5s), `TransitionBase7` (2s), `TransitionBase8` (2.5s), `TransitionBase9` (3s), `TransitionBase10` (5s), `TransitionBase11` (10s)
- Fade helpers
  - `FadeOut0_05`, `FadeOut0_1`, `FadeOut0_25`, `FadeOut0_5`, `FadeOut0_75`, `FadeOut1`, `FadeOut1_5`, `FadeOut2`, `FadeOut2_5`, `FadeOut3`, `FadeOut5`, `FadeOut10`
- One stylesheet
  - Located at `Resources/UnityEssentials_USS_Transition.uss` for easy load at edit/runtime

## Requirements
- Unity 6000.0+
- UI Toolkit (UXML/USS)

## Usage

### 1) Add the stylesheet to your UI
- In UI Builder or UXML: include the stylesheet `Resources/UnityEssentials_USS_Transition.uss`
- Or via C# at runtime:
```csharp
using UnityEngine;
using UnityEngine.UIElements;

var root = GetComponent<UIDocument>().rootVisualElement;
var transitions = Resources.Load<StyleSheet>("UnityEssentials_USS_Transition");
root.styleSheets.Add(transitions);
```

### 2) Choose a transition preset on the element
Pick one `TransitionBase*` class to define duration/ease:
```csharp
element.AddToClassList("TransitionBase2"); // 0.25s ease-in-out
```

### 3) Toggle state classes to animate
Add/remove utility classes to transition into/out of states:
```csharp
// Slide in from left and fade in
element.AddToClassList("TranslateLeftX");
element.AddToClassList("Opacity0");
// later, show
element.RemoveFromClassList("TranslateLeftX");
element.RemoveFromClassList("Opacity0");

// Hide with quick fade
element.AddToClassList("FadeOut0_25");
```

### 4) Combine transforms
You can mix translate/scale/rotate with opacity for richer motion:
```csharp
// Pop-in: start small and transparent
el.AddToClassList("Scale0_9");
el.AddToClassList("Opacity0");
el.AddToClassList("TransitionBase1");
// then remove to animate back to normal
el.RemoveFromClassList("Scale0_9");
el.RemoveFromClassList("Opacity0");
```

## How It Works
- The stylesheet sets `transition-property: opacity, translate, scale, rotate` with the chosen duration(s) and `ease-in-out`
- State classes define target values for those properties (e.g., `translate: -100 0;`, `opacity: 0;`, `scale: 1.1 1.1;`)
- When you add or remove a state class, UI Toolkit animates from the current value to the new one according to the selected `TransitionBase*`

## Notes and Limitations
- Apply exactly one `TransitionBase*` per element for predictable results
- Units
  - Translate values are in pixels; `*X` variants use large distances (~1500 px) for off‑screen motion
  - Rotate uses degrees (e.g., `45deg`), except `Rotate0` which resets rotation
- Fade helpers set `opacity: 0` with their own duration; combine with translate/scale/rotate as needed
- Performance: excessive simultaneous transitions can impact frame time - prefer shorter presets and minimal affected elements
- Customization: duplicate the USS into your project if you want to tweak durations or add your own utility classes

## Files in This Package
- `Resources/UnityEssentials_USS_Transition.uss` – Utility transitions and transform classes
- `package.json` – UPM metadata
- `LICENSE.md` – License

## Tags
unity, ui toolkit, uitoolkit, uss, transition, animation, opacity, translate, scale, rotate, ease-in-out
