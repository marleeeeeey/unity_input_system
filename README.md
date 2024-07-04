# Unity Learning - Input System

## Course Information

### Main Course Information

- Course: "The Ultimate Input System with Rebinding in Unity".
- Course link: https://www.udemy.com/course/unity-input-system-rebind/
- Repository: #TODO
- Course duration: 5 hour(s).
- Created by: Mario Korov - https://www.linkedin.com/in/mario-korov-ab9590202/
- Assets:
  - #TODO
- Slides
  - #TODO

### What we will learn in this course

https://docs.unity3d.com/Packages/com.unity.inputsystem@1.7/manual/Workflows.html

- Learn how to use the new Unity Input System workflows:
  - Directly Reading Device States.
  - Using Embedded Actions.
  - Using an Actions Asset.
  - Using an Actions Asset and a PlayerInput component.

## My goals

- Planned course range: 2024-07-04 - 2024-07-06 = 3 day(s) and 4.5 hour(s) of course.
- Actual course range: 2024-07-04 - #TODO = #TODO day(s)
- Learning goal: #TODO

## Tasks

### TODO list

- #TODO

## Gogot vs Unity

- (Plus to Unity) Unity has possibility to attach several scripts to the one object.

## Unity Guidelines from the Course

- #TODO

## Unity Recipes from the Course

### Import the Input System

- `Window -> Package Manager -> Input System -> Install`.
- `Edit -> Project Settings -> Player -> Other Settings -> Active Input Handling -> Input System Package (New) or Both`.

### Workflow 1: Directly Reading Device States

- https://docs.unity3d.com/Packages/com.unity.inputsystem@1.7/manual/Workflow-Direct.html
- Include `using UnityEngine.InputSystem;`.
- Use `Keyboard`, `Gamepad`, `Mouse` or `Touchscreen` classes.
- Use `Keyboard.current.wKey.isPressed` to check if the key is pressed.
- Use `Keyboard.current.wKey.wasPressedThisFrame` to check if the key was pressed this frame.
- Use `Keyboard.current.wKey.wasReleasedThisFrame` to check if the key was released this frame.
