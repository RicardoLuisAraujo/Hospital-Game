# Haunted Hospital - Implementation Plan

This plan outlines the initial architecture for a 3D game featuring a dual-mode gameplay: Day (Adventure/Visual Novel) and Night (Puzzle/Platformer).

## 1. Game State Management
A global Game Manager will handle the transition between "Day" and "Night" states. This will control:
- Player controls (Dialogue vs. Movement/Action)
- Camera behavior
- UI visibility
- Environmental lighting/mood

## 2. Day Mode (Core Focus)
- **Character Controller:** Modern 3D controller (Persona/Catherine style).
- **Camera System:** Cinemachine with a follow camera.
- **Interaction:** Trigger-based interaction for NPCs.

## 3. Dialogue System (Custom C#)
We will build a ScriptableObject-based system for conversations. This allows us to:
- Define dialogues as assets in the Project window.
- Store branching logic and player choices.
- Easily hook into the UI.

## 4. Night Mode (Exploration/Puzzle)
- **Gameplay Transition:** Switches to a puzzle or platformer focus.
- **Mechanics:** Inventory-based puzzles or survival elements.
- **Atmosphere:** Pivot from bright, social "Day" to dark, uncanny "Night".

## 5. Technical Foundations [IMPORTANT]
- **Render Pipeline:** Universal Render Pipeline (URP) for high-quality visuals and performance.
- **Input System:** Unity's New Input System for smooth switching between control schemes.
- **Assets:** Modular environment assets and curated lighting presets.

---

## User Review Required
> **Current Direction:** We are prioritizing the "Day" mode character movement and the custom dialogue system first.

## Verification Plan
### Manual Verification
- [ ] Prototype the Day/Night transition in a simple scene.
- [ ] Test basic character movement and camera switching.
- [ ] Verify dialogue trigger works and pauses player movement.
- [ ] Confirm lighting shifts correctly between Day and Night.