using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    public Vector3 playerVelocity;

    public float speed = 5f;
    public bool isGrounded;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
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
            }

        }
    }
}
