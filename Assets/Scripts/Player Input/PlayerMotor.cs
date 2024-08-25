using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private AudioSource JumpSound;
    private CharacterController controller;
    public Vector3 playerVelocity;

    public float speed = 5f;
    public bool isGrounded;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private bool isFalling = false;

    public AudioSource WalkingSound;
    public AudioSource LandingSound;
    private Vector3 previousPosition;
    private float fallTimer;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        previousPosition = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        Falling();
        HandleWalkingSound();
        previousPosition = transform.position;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);


    }

    public void Jump(InputAction.CallbackContext ctxt)
    {

        if (ctxt.performed)
        {
            if (isGrounded)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
                JumpSound.Play();
            }
            if (WalkingSound.isPlaying)
            {
                WalkingSound.Stop();
            }


        }
    }
    public void Falling()
    {
        if (!isGrounded && playerVelocity.y < 0) //when the y velocity is below 0 the player is accelerating down which is falling
        {
            isFalling = true;

        }

        // Check if the player lands on the ground
        if (isGrounded && isFalling) //it must be on the ground and that it's y velocity <0(the instant that it lands it's velocity is still below 0 thus this works)
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


        if (isGrounded && hasMoved && !isFalling && fallTimer <= 0)  //this is so that the player must be on the ground, moving, hasn't just landed, and a slight delay after landing so that the landing sound effect plays first before walking sound effect
        {
            WalkingSound.Play(); //playing the walking sound effect

        }


    }
}
