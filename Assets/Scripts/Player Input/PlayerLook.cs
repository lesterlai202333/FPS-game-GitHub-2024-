using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam; //declaring a camera component
    private float xRotation = 0f;//float variable xRotation set to 30

    public float xSensitivity = 30f;//float variable xsensitivity set to 30
    public float ySensitivity = 30f; //float variable ysensitivity set to 30
    private float xScale = 0; //float variable scale x set to 0
    void Start()
    {
        //locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x; //setting the mouseX variable to the x-coordinate of the user's input(mouse cursor)
        float mouseY = input.y;//setting the mouseY variable to the y-coordinate of the user's input(mouse cursor)

        //adjusts the camera's vertical rotation based on mouse input, scaled by sensitivity and time.
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        //clamps the range of rotation so you can't look too far up or down so the experience is natural and controlled.
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); //Mathf.Clamp restricts a specific variable to a range(xRotation to (-80, 80))

        //sets the camera's rotation so that it tilts up or down based on the xRotation value, which was calculated using the player's mouse input. Quaternion.Euler is a function that converts the rotation given in Euler angles (measured in degrees) to a quaternion, which is a format Unity uses to handle rotations. transform.localRotation is a property that sets or gets the rotation of a game object relative to its parent. In this case, it is setting the camera's rotation.
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0); //rotation around y and z are set to zero so only rotates along x axis which is up and down. X-rotation is rotating along x axis

        //his line of code rotates the player horizontally (left or right) based on the horizontal movement of the mouse (mouseX).
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity); ////transform.Rotate rotates the player around the y axis(so turning left/right) according to the horizontal mouse input
        //the rotation scaled by both the time between frames (Time.deltaTime so independent of framerate) to ensure smooth motion and a sensitivity factor (xSensitivity) to adjust how fast the player turns in response to the mouse input.
    }

}
