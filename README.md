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
-
- Include `using UnityEngine.InputSystem;`.
- Use `Keyboard`, `Gamepad`, `Mouse` or `Touchscreen` classes.
- Use `Keyboard.current.wKey.isPressed` to check if the key is pressed.
- Use `Keyboard.current.wKey.wasPressedThisFrame` to check if the key was pressed this frame.
- Use `Keyboard.current.wKey.wasReleasedThisFrame` to check if the key was released this frame.

- `Edit -> Project Settings -> Input System Package -> Create Settings Asset` to create an input settings asset.

### Workflow 2: Using Embedded Actions

- https://docs.unity3d.com/Packages/com.unity.inputsystem@1.7/manual/Workflow-Embedded.html
- provides you with the flexibility to modify or add multiple bindings without needing to change your code.
  - `CallbackContext.performed` - is called when the action is performed.
  - `CallbackContext.canceled` - is called when the action is canceled.
  - `CallbackContext.started` - is called when the action is started.
- To read `Vector2` values
  - In Inspector, add a new binding to the action called `Add Up/Down/Left/Right Composite Binding`.
  - In the code, use `context.ReadValue<Vector2>()` to read the value.
- To read Gamestick values
  - In Inspector, add a new binding to the action called `Add Binding`.
  - In the code, use `context.ReadValue<Vector2>()` to read the value.
- Use `Add Positive/Negative Bundings` to support only one direction.
- `Edit -> Project Settings -> Input System Package -> Default Deadzone` to set the deadzone for the gamestick and even make it workable discrete like a keyboard.

```
using UnityEngine;
using UnityEngine.InputSystem;

public class ExampleScript : MonoBehaviour
{
    public InputAction move;
    public InputAction jump;
}
```

![Workflow 2: Using Embedded Actions](screenshots/README_image.png)

```csharp

public class EmbeddedActions : MonoBehaviour
{
    public InputAction jumpAction;

    void Start()
    {
        jumpAction.performed += OnPerformed;
        jumpAction.canceled += OnCanceled;
        jumpAction.started += OnStarted;
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        jumpAction.performed -= OnPerformed;
        jumpAction.canceled -= OnCanceled;
        jumpAction.started -= OnStarted;
        jumpAction.Disable();
    }

    private void OnPerformed(InputAction.CallbackContext context)
    {
        bool value = context.ReadValueAsButton();
        Debug.Log("[EmbeddedActions.OnPerformed]: " + value);
    }

    private void OnCanceled(InputAction.CallbackContext context)
    {
        bool value = context.ReadValueAsButton();
        Debug.Log("[EmbeddedActions.OnCanceled]: " + value);
    }

    private void OnStarted(InputAction.CallbackContext context)
    {
        bool value = context.ReadValueAsButton();
        Debug.Log("[EmbeddedActions.OnStarted]: " + value);
    }

}

```
