# Unity Learning - Input System

## Course Information

### Main Course Information

- Course: "The Ultimate Input System with Rebinding in Unity".
- Course link: https://www.udemy.com/course/unity-input-system-rebind/
- Course duration: 5 hour(s).
- Created by: Mario Korov - https://www.linkedin.com/in/mario-korov-ab9590202/

### What we will learn in this course

https://docs.unity3d.com/Packages/com.unity.inputsystem@1.7/manual/Workflows.html

- Learn how to use the new Unity Input System workflows:
  - Directly Reading Device States.
  - Using Embedded Actions.
  - Using an Actions Asset.
  - Using an Actions Asset and a PlayerInput component.

## My goals

- Planned course range: 2024-07-04 - 2024-07-06 = 3 day(s) and 4.5 hour(s) of course.
- Actual course range: 2024-07-04 - 2024-07-06 = 3 day(s)
- Learning goal: Learn how to use the new Unity Input System workflows.

## Tasks

### TODO list

- Complete the section `Rebinding`.

## Unity Guidelines from the Course

- Use `OnEnable`, `OnDisable` and `OnDestroy` to enable and disable the input actions.
  - It may be useful to implement menus and pause the game.

## Unity Recipes from the Course

### Import the Input System

- `Window -> Package Manager -> Input System -> Install`.
- `Edit -> Project Settings -> Player -> Other Settings -> Active Input Handling -> Input System Package (New) or Both`.

### Workflow 1: Directly Reading Device States

![Workflow 1](screenshots/README_image-14.png)

- https://docs.unity3d.com/Packages/com.unity.inputsystem@1.7/manual/Workflow-Direct.html
-
- Include `using UnityEngine.InputSystem;`.
- Use `Keyboard`, `Gamepad`, `Mouse` or `Touchscreen` classes.
- Use `Keyboard.current.wKey.isPressed` to check if the key is pressed.
- Use `Keyboard.current.wKey.wasPressedThisFrame` to check if the key was pressed this frame.
- Use `Keyboard.current.wKey.wasReleasedThisFrame` to check if the key was released this frame.
- Use `Keyboard.current.wKey.wasPerformedThisFrame` to check if the key was performed this frame. All `interactions` are completed.

- `Edit -> Project Settings -> Input System Package -> Create Settings Asset` to create an input settings asset.

### Workflow 2: Using Embedded Actions

![Workflow 2](screenshots/README_image-13.png)

- https://docs.unity3d.com/Packages/com.unity.inputsystem@1.7/manual/Workflow-Embedded.html
- provides you with the flexibility to modify or add multiple bindings without needing to change your code.
  - `CallbackContext.started` - is called when the action is starte - when the button is pressed.
  - `CallbackContext.performed` - is called when the action is performed - all `interactions` are completed.
  - `CallbackContext.canceled` - is called when the action is canceled.
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

### Workflow 3: Using an Actions Asset

![Workflow 3](screenshots/README_image-12.png)

**Theory**

- https://docs.unity3d.com/Packages/com.unity.inputsystem@1.7/manual/Workflow-ActionsAsset.html
- Allows you to keep the data that defines your actions separate from the GameObjects.
- All your action definitions are stored as a single asset file.
- Ability to group actions into Action Maps and Control Schemes.
- Input Actions Asset window separate to four parts:
  - `Control Scheme` - a collection of action maps.
    - You might have one control scheme which is "Joypad", and
    - another control scheme which is "Keyboard and Mouse".
    - used to adapt the in-game UI to show the correct keys or buttons in on-screen prompts.
  - `Action Map` - a collection of actions.
    - each map relates to a different situation:
      - driving vehicles and
      - navigating on foot, and
      - may have in-game menus.
  - `Action` - a single action.
  - `Binding` - a single binding.
- There are **two distinct ways to access it from your code**. You can either:
  - Use an **inspector reference** to the Actions Asset.
    - You must read the actions by name using strings.
  - **Generate a C# class** that wraps your Actions Asset.
    - Unity generates an accompanying class as a new .cs script asset

**Practice**

- `Project -> Create -> Input Actions` to create an actions asset.
  - Call it `PlayerControls`.
  - Double click on it to open the asset.

![Example of Input Actions Asset](screenshots/README_image.png)

- `Action Types` may be `Value`, `Button` or `Pass-Through`.
- `Control Type` may have a lot of options. Uses to filter the input.
  - Choose `Any` to accept any input. Then all types of bindings will be available:

![all types of bindings](screenshots/README_image-1.png)

- Moslty you may use `All Binding` to accept all types of bindings.

- Example of Positive/Negative Binding:

![Example of Positive/Negative Binding](screenshots/README_image-2.png)

- Adding control scheme and setup the requirements:

![Adding control scheme](screenshots/README_image-3.png)

![Using control schemes](screenshots/README_image-4.png)

- `Control Scheme` is useful when you will be implement the reseting of the bindings.

### Workflow 3.1: Using an Actions Asset Via Inspector Reference

- `[SerializeField] InputActionAsset inputActions;`
- Drag and drop the `PlayerControls` asset to the `inputActions` field.

```csharp

    [SerializeField] InputActionAsset actionAsset;

    InputAction jumpAction;
    InputAction moveAction;
    InputAction attackAction;
    InputActionMap playerNormalMap;

    private void Awake()
    {
        playerNormalMap = actionAsset.FindActionMap("PlayerNormal");
        jumpAction = playerNormalMap.FindAction("Jump");
        moveAction = playerNormalMap.FindAction("MoveHorizontal");
        attackAction = playerNormalMap.FindAction("Attack");
    }

```

- Use `OnEnable`, `OnDisable` and `OnDestroy` to enable and disable the input actions.
  - It may be useful to implement menus and pause the game.
- You may `Enable/Disable` whole `Asset`, `Action Map` or `Action`.

```csharp

    private void OnEnable()
    {
        actionAsset.Enable(); // Enable the entire asset
        // playerNormalMap.Enable(); // Enable individual maps
        // jumpAction.Enable(); // Enable individual actions
    }

```

- Use `moveAction.ReadValue<float>();` to read one demensional value.
- Use `moveAction.ReadValue<Vector2>();` to read two demensional value.
- Use `context.ReadValueAsButton();` to read the value as a button (bool) in callbacks.

```csharp

    private void OnEnable()
    {
        jumpAction.performed += JumpExample;
        jumpAction.canceled -= JumpStopExample;
        actionAsset.Enable();
    }

    private void OnDisable()
    {
        actionAsset.Disable();
        jumpAction.performed -= JumpExample;
        jumpAction.canceled -= JumpStopExample;
    }

    private void JumpExample(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping");
        // tryToJump = context.ReadValueAsButton();
        tryToJump = true;
    }

    private void JumpStopExample(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping Stopped");
        tryToJump = false;
    }

```

### Workflow 3.2: Using an Actions Asset Via Generated C# Class

- `Project -> Create -> Input Actions` to create an actions asset and named `PlayerControls`.
- Select the `PlayerControls` asset and click on the `Generate C# Class` button in the inspector.
- Use this class in the code.

```csharp

    PlayerControls controls; // Generated class from the input system

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.PlayerNormal.Jump.performed += JumpExample;
        controls.PlayerNormal.Jump.canceled += JumpStopExample;

        controls.PlayerNormal.Enable();
    }

    private void OnDisable()
    {
        controls.PlayerNormal.Disable();

        controls.PlayerNormal.Jump.performed -= JumpExample;
        controls.PlayerNormal.Jump.canceled -= JumpStopExample;
    }

    private void Update()
    {
        valueX = controls.PlayerNormal.MoveHorizontal.ReadValue<float>();
        Debug.Log("Value X: " + valueX);
    }

    private void JumpExample(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping");
        // tryToJump = context.ReadValueAsButton();
        tryToJump = true;
    }

    private void JumpStopExample(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping Stopped");
        tryToJump = false;
    }

```

### Ground check via 2D Ray cast

- Use `Physics2D.Raycast(position, direction, distance, layerMask);` to check the ground.

```csharp
RaycastHit2D hit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, detectLayer);
if (hit)
{
    Debug.Log("Grounded");
}
```

### Basic Animations

- Select the object in the hierarchy.
- Open the `Animation` window.
- Click `Play` to start the animation.

### Blend Tree and Jump Animation

- Remove `JumpUp` and `JumpDown` animations from the `Animator`.
- Create a new `Blend Tree` and add `JumpUp` and `JumpDown` animations. Name it `JumpBlendTree`.
- Double click on the `JumpBlendTree` to open it.
- Select parameters `SpeedY` in `JumpBlendTree -> Parameters`.
- Add `JumpUp` and `JumpDown` animations into `JumpBlendTree -> Motion`.

![Blend Tree](screenshots/README_image-5.png)

![Animator Example](screenshots/README_image-6.png)

- For `Attack Animation` make sense to disable `Can Transition to Self` in the `Animator`.

### Call method at the end of the animation

![Call method at the end of the animation](screenshots/README_image-7.png)

![Set the callback method](screenshots/README_image-8.png)

### Interactions and Processors

- `Interactions` are used to filter the input.
  - `Hold` - is used to check if the button is held some time. Action starts when the time is reached.
  - `Tap` - is used to check if the button is tapped (pressed and released quickly).
  - `Multi-Tap` - is used to check if the button is tapped multiple times.

![Interactions](screenshots/README_image-9.png)

- `Processors` are used to modify the input.
  - Order is important. Processors are executed from the top to the bottom.
  - `Processors` list is dependent on the `Control Type`.

![Processors](screenshots/README_image-10.png)

### Started vs Performed

- `Started` is called when the action is started.
- `Performed` is called when all `interactions` are completed.

### Actions with Modifiers

- Use `Add Binding With One/Two Modifiers` to add the modifiers.
- `Edit -> Project Settings -> Input System Package -> Enable Input Consumption` to
  - Buttons with modifiers will have more priority then buttons without modifiers.
  - Prevent sumultaneous actions like `Shift + W` and `W`.
  - Be careful with this option.

### Workflow 4: Using an Actions Asset and a PlayerInput component

![Workflow 4](screenshots/README_image-11.png)

- provides the flexibility of being able to connect any action to any public method on a GameObject’s component without writing code.

**Practice**

- Create `InputActions` asset. Call it `Controls`.
- Add `2D Vector Composite Binding` to the `Move` action.
  - `Didital Normalized` is used to normalize the input from keyboard for 2D Vector.
- `Add Bindings` for the gamepad `Move` action.
  - Add `Deadzone` to the gamepad `Move` action.
  - Add `2D Normalized` to the gamepad `Move` action.
- `Add Control Scheme` and setup the requirements.
  - Add `Keyboard and Mouse` and `Gamepad` control schemes.
- Add `Player Input` component to the player game object.
  - Assign the `Controls` asset to the `Actions` field.

### Workflow 4.1: `Send Message` behavior

- Create callback methods names mirroring the action names in the `Controls` asset.
  - `OnMove` for the `Move` action.
  - `OnJump` for the `Jump` action.

![Send Message](screenshots/README_image-15.png)

```csharp

using UnityEngine.InputSystem;

    void OnMove(InputValue value)
    {
        Debug.Log("OnMove: " + value.Get<Vector2>());
    }

```

### Workflow 4.2: `Broadcast Message` behavior

- broadcast message will call the passed in function for all the scripts on the game object that have that function and **for all children objects** that also have that function.

### Workflow 4.3: `Invoke Unity Event` behavior

![Invoke Unity Event](screenshots/README_image-16.png)

- Allow to set object-reciever and method to call.
- Possible to setup several objects and methods to call.
- Callbacks signature: `public void MoveExample(InputAction.CallbackContext value)`.
- All three fazes were called: `Started`, `Performed` and `Canceled`.

```csharp

    public void AttackExample(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            Debug.Log("Attack performed");
        }
        else if (value.canceled)
        {
            Debug.Log("Attack canceled");
        }
        else if (value.started)
        {
            Debug.Log("Attack started");
        }
    }

```

### Workflow 4.4: `Invoke C# Event` behavior

- Use `SwitchCurrentActionMap` to switch the action map.

```csharp

    PlayerInput playerInput;
    InputActionMap playerBasicInputMap;
    InputAction attackAction;
    InputAction moveAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerBasicInputMap = playerInput.actions.FindActionMap("PlayerBasic");
        attackAction = playerBasicInputMap.FindAction("Attack");
        moveAction = playerBasicInputMap.FindAction("Move");
    }

    private void OnEnable()
    {
        attackAction.performed += OnAttackPerformed;
        attackAction.canceled += OnAttackCanceled;
        playerBasicInputMap.Enable();
    }

    private void Update()
    {
        direction = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

```

### Move animation

![Base Layer Animation](screenshots/README_image-18.png)

![Idle Blend Tree](screenshots/README_image-17.png)

### Handle UI Input. Menus.

- Create UI Button. It will create also `Canvas` and `Event System`.
- Open the `Event System` and `Replace with Input System UI Input Module`.
- `Event System -> Deselect on Background Click` set to `false`.
- `Inputactions -> Action Type -> Pass Through` means that the input will be passed from all connected devices.
- Drag and drop `Event System` to the `Player Input -> UI Input Module` field.
- Use `playerInput.currentActionMap` to get the current action map.

![InputActions Menu Setup](screenshots/README_image-19.png)

```csharp

    private void OnEnable()
    {
        menuActivateDeactivateAction.performed += MenuControl;

        uiInputMap.Disable();
        playerBasicInputMap.Enable();
        mapSwitcherInputMap.Enable();
    }

    InputActionMap placeholderInputMap;
    private void MenuControl(InputAction.CallbackContext context)
    {
        Debug.Log("Menu activated/deactivated");

        if (menuObject == null)
        {
            Debug.LogError("Menu Canvas is not assigned");
            return;
        }

        if (placeholderInputMap == null)
        {
            placeholderInputMap = playerInput.currentActionMap;
        }

        if (menuObject.activeSelf)
        {
            menuObject.SetActive(false);
            playerInput.SwitchCurrentActionMap(placeholderInputMap.name);
            placeholderInputMap = null;
        }
        else
        {
            menuObject.SetActive(true);
            playerInput.SwitchCurrentActionMap(uiInputMap.name);
        }
    }

```
