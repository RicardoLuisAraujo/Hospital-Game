# Haunted Hospital - System Walkthrough

I have implemented the core technical architecture for "Haunted Hospital", focusing on the dual-mode gameplay and the "Persona-style" interaction system.

## 🛠 Implemented Systems

### 1. Global Game Management
The `GameManager` orchestrates the entire game flow. It uses a singleton pattern and event-based architecture to notify other systems (like lighting and camera) when the phase changes.

- **Files:** [GameManager.cs](file:///Users/ricardoaraujo/Hospital%20Game/Assets/Scripts/Core/GameManager.cs), [PhaseSwitcher.cs](file:///Users/ricardoaraujo/Hospital%20Game/Assets/Scripts/Core/PhaseSwitcher.cs)
- **Feature:** Press **'T'** in Play Mode to toggle between **Day** and **Night**.

### 2. Modern 3D Character Controller
Built a smooth, camera-relative movement system inspired by modern social RPGs.
- **Files:** [PlayerController.cs](file:///Users/ricardoaraujo/Hospital%20Game/Assets/Scripts/Characters/PlayerController.cs)
- **Features:** 
  - Rotation smoothing (character turns gracefully).
  - Camera-relative input (up is always "away from camera").
  - Movement is automatically disabled during dialogue.

### 3. Branching Dialogue System
A customizable system using Unity ScriptableObjects for easy content creation.
- **Files:** [DialogueData.cs](file:///Users/ricardoaraujo/Hospital%20Game/Assets/Scripts/Dialogue/DialogueData.cs), [DialogueManager.cs](file:///Users/ricardoaraujo/Hospital%20Game/Assets/Scripts/Dialogue/DialogueManager.cs), [StateAwareDialogueTrigger.cs](file:///Users/ricardoaraujo/Hospital%20Game/Assets/Scripts/Dialogue/StateAwareDialogueTrigger.cs)
- **Features:**
  - **Dynamic Variants:** The `StateAwareDialogueTrigger` can play different conversations depending on whether it's Day or Night (e.g., an NPC being friendly by day but uncanny by night).
  - **Choices:** Supports multiple player choices that can lead to different dialogue branches.

### 4. Atmospheric Transition Manager
Handles the visual shift between phases without needing complex scene reloads.
- **Files:** [EnvironmentMoodManager.cs](file:///Users/ricardoaraujo/Hospital%20Game/Assets/Scripts/Environment/EnvironmentMoodManager.cs)
- **Features:**
  - Transitions directional light color and intensity.
  - Lerps ambient lighting and skybox settings for a smooth "sunset/uncanny" effect.

## 🚀 Unity Editor Setup Guide

### 1. Setting up the Global Systems
1. **Create the Object:** In the **Hierarchy** window (left side), right-click in an empty space and select **Create Empty**. Name it `_Systems`.
2. **Add GameManager:** With `_Systems` selected, go to the **Inspector** (right side) and click **Add Component**. Search for `GameManager` and select it.
3. **Add PhaseSwitcher:** Click **Add Component** again, search for `PhaseSwitcher`, and add it. 
4. **Add DialogueManager:** Click **Add Component** and add the `DialogueManager` script here as well.

### 2. Setting up the Player
1. **Identify the Player:** Select your Player character model (or a simple Cube for testing) in the Hierarchy.
2. **Add Physics:** Click **Add Component** and search for `Character Controller`. Ensure the green capsule capsule fits your character.
3. **Add Controller:** Click **Add Component** and add the `PlayerController` script.
4. **Assign Camera:** In the `PlayerController` component, find the **Camera Transform** field. Drag your **Main Camera** from the Hierarchy into this slot.

### 4. Configuring the Lighting Transition
1. **Attach Script:** Add the `EnvironmentMoodManager` script to your `_Systems` GameObject.
2. **Link the Light:** In the Inspector for `EnvironmentMoodManager`, find the **Main Directional Light** slot and drag your scene's **Directional Light** into it.
3. **Set the Colors (Crucial):** By default, Unity sets colors to black. You must set them manually:
   - **Day Mood:** Set `Light Color` to **White/Warm Yellow** and `Light Intensity` to **1.0**.
   - **Night Mood:** Set `Light Color` to **Cool Blue/Purple** and `Light Intensity` to **0.3**.
   - *Repeat for Ambient Color (Sky lighting).*

> [!IMPORTANT]
> If the colors are not set in the Inspector, the light will simply turn black when you switch phases!

> [!TIP]
> Use the **'T'** key in Play Mode to instantly see the lighting transition and test if your scripts are listening to the GameManager!
