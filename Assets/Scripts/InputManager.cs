using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput; // Private field to store an instance of the PlayerInput class, which handles player input. This field is not accessible outside of this script.
    public PlayerInput.OnFootActions onFoot; // Public field for accessing input actions related to movement and actions performed on foot. This can be accessed from outside of this script.
    public PlayerMotor motor; // Public field to reference the PlayerMotor script, which handles player movement. This can be accessed from outside of this script.
    public PlayerLook look; // Public field to reference the PlayerLook script, which handles the player's looking direction. This can be accessed from outside of this script.
    void Awake()
    {
        playerInput = new PlayerInput(); // Initializes the playerInput instance with a new PlayerInput object, setting up input handling.
        onFoot = playerInput.onFoot; // Assigns the onFoot actions from the PlayerInput instance to the onFoot field.
        motor = GetComponent<PlayerMotor>(); // Retrieves and assigns the PlayerMotor component attached to the same GameObject as this script.
        look = GetComponent<PlayerLook>(); // Retrieves and assigns the PlayerLook component attached to the same GameObject as this script.
        onFoot.Jump.performed += ctx => motor.Jump(ctx); // Sets up an event listener for the Jump action. When the Jump action is performed, it calls the Jump method of the PlayerMotor with the context (ctx) of the input action.
    }

    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>()); // Reads the current movement input values as a Vector2 and passes them to the ProcessMove method of PlayerMotor. This is done in FixedUpdate for smooth physics interactions.
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.LookAround.ReadValue<Vector2>()); // Reads the current look input values as a Vector2 and passes them to the ProcessLook method of PlayerLook. This is done in LateUpdate to ensure it occurs after all other updates for accurate camera positioning.
    }

    private void OnEnable()
    {
        onFoot.Enable(); // Enables the onFoot input actions, making them active and ready to receive input.
    }

    public void OnDisable() // Public so can be called from outside the script to disable the input actions.
    {
        onFoot.Disable(); // Disables the onFoot input actions, making them inactive and stopping them from receiving input.
    }
}