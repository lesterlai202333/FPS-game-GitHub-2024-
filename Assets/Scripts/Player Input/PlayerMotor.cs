using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private AudioSource JumpSound; //private means cannot be accessed out of script howveer due to the serialized field it can be accessed in the unity inspector. The type of variable is an audiosource
    private CharacterController controller; //declaring an audiosource and a charactercontroller that I can assign in the unity editor.
    public Vector3 playerVelocity;//public --> can be accessed from outside of this script

    public float speed = 5f; //public --> can be accessed from outside of this script
    public bool isGrounded;//public --> can be accessed from outside of this script
    public float gravity = -9.81f;//public --> can be accessed from outside of this script
    public float jumpHeight = 3f;//public --> can be accessed from outside of this script
    private bool isFalling = false;

    public AudioSource WalkingSound;//public --> can be accessed from outside of this script
    public AudioSource LandingSound;//public --> can be accessed from outside of this script
    private Vector3 previousPosition;
    public float landingSoundTimer = 1f;//public --> can be accessed from outside of this script
    private float fallTimer; //declaring variables. public variables can be accessed in other scripts, private variables cannont.
    void Start()
    {
        controller = GetComponent<CharacterController>(); //accessing information of the character controlelr when the scene is started
        previousPosition = transform.position; //sets the previousPosition variable to the current position of the player
    }

    private void Awake()
    {
        if (landingSoundTimer > 0)
        {
            LandingSound.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (landingSoundTimer > 0)
        {
            landingSoundTimer -= Time.deltaTime;
        }
        else
        {
            LandingSound.enabled = true;
        }
        isGrounded = controller.isGrounded; //checks if the character or object controlled by the controller is currently touching the ground. It then stores this boolean value (true or false) in the isGrounded variable.
        Falling(); //calls the falling function constantly
        HandleWalkingSound();//calls the handlewalkingsound function constantly
        previousPosition = transform.position; //constantly sets the previousPosition variable to the current position of the player
    }

    public void ProcessMove(Vector2 input) //public --> can be accessed from outside of this script, void means no value return, the function processes move and only accepts parameters of Vector 2 from player's input
    {
        Vector3 moveDirection = Vector3.zero; //A Vector3 named moveDirection is initialized to Vector3.zero, meaning it starts with (0, 0, 0).
        moveDirection.x = input.x; //The x component of moveDirection is set to the x value from input(keyboard input for horizontal and vertical movement).
        moveDirection.z = input.y; //The z component of moveDirection is set to the y value from input(keyboard input for horizontal and vertical movement). This assumes a standard 3D movement where x is for horizontal movement (left-right), and z is for vertical movement (forward-backward).
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime); //transform.TransformDirection(moveDirection) converts the local direction (moveDirection) into world space direction based on the character's orientation. speed is a multiplier to control how fast the character moves.Time.deltaTime ensures the movement is frame-rate independent. controller.Move(...) actually moves the character in the game world according to the computed direction and speed.
        playerVelocity.y += gravity * Time.deltaTime; //gravity is applied to y velocity so simulates the gravity pull on the player
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f; //if the character is on the ground (isGrounded) and if the y velocity is negative (falling). If both conditions are true, the y velocity is set to - 2f, ensuring the character stays grounded or lands smoothly, preventing it from continually falling or being stuck in a falling state.
        controller.Move(playerVelocity * Time.deltaTime); //this line applies all the code above to the player sprite. It moves the character based on their current velocity independent of framerate so it's smooth.


    }

    public void Jump(InputAction.CallbackContext ctxt) //InputAction.CallbackContext ctxt provides context and details about the input action. This has got to do with the keybind, so if the key corresponding to jump is pressed then the stuff inside this function would be played. public --> can be accessed from outside of this script, void means no value returned
    {

        if (ctxt.performed) // Checks if the input action has been successfully performed, ensuring that the associated code only runs in response to the actual input event.
        {
            if (isGrounded) // initiates a jump by setting the character’s vertical velocity based on the desired jump height and gravity. It also plays a jump sound effect and stops the walking sound if it’s playing.
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
                JumpSound.Play();
            }
            if (WalkingSound.isPlaying)
            {
                WalkingSound.Stop();
            }

            //so for example the player only jumps when the player presses space., as it's provided as part of the context here: (InputAction.CallbackContext ctxt)
        }
    }
    public void Falling()//public --> can be accessed from outside of this script, void means no value returned.
    {
        if (!isGrounded && playerVelocity.y < 0) //condition of when the y velocity is below 0 the player is accelerating down and the player isn't grounded so not on a slope, the is falling variable is now true             
        {
            isFalling = true;

        }

        // Check if the player lands on the ground
        if (isGrounded && isFalling) //it must be on the ground and that it's y velocity <0(the instant that it lands it's velocity is still below 0 thus this works
        {
            isFalling = false;
            LandingSound.Play(); // Play landing sound when landing
            fallTimer = 0.3f; //Value of the timer

        }
        if (fallTimer > 0) //this is a countdown timer
        {
            fallTimer -= Time.deltaTime;
        }
    }

    private void HandleWalkingSound()

    {
        // Check if the player has moved
        bool hasMoved = (transform.position - previousPosition).magnitude > 0.1f; //this measures the channge in position of the player, if there is a change the bool would be true so the player is moving


        if (isGrounded && hasMoved && !isFalling && fallTimer <= 0)  //this is so that the player must be on the ground, moving, hasn't just landed, and a slight delay after landing so that the landing sound effect plays first before walking sound effect which makes the sound transition more realistic and fluent
        {
            WalkingSound.enabled = true; //playing the walking sound effect

        }
        else
        {
            WalkingSound.enabled = false; //stops the walking sound effect if the player wasn't walking(stationary, in the air). 
        }



    }
}
