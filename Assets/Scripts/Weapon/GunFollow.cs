using UnityEngine;

public class GunFollow : MonoBehaviour

{
    public Camera playerCamera;   // Assign the player's camera in the Inspector
    public Vector3 gunOffset;   // Offset to keep the gun in a fixed position relative to the camera
    void Start()
    {
        transform.position = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y - 0.31f, playerCamera.transform.position.z + 0.02f) + playerCamera.transform.TransformDirection(gunOffset);

        transform.rotation = playerCamera.transform.rotation;
    }

    // Update is called once per frame

    void Update()
    {
        // Update the gun's position and rotation every frame to follow the camera
        FollowCamera();
    }

    void FollowCamera()
    {
        // Set the gun's position to the camera's position plus the offset
        transform.position = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y - 0.31f, playerCamera.transform.position.z + 0.7f) + playerCamera.transform.TransformDirection(gunOffset);

        // Align the gun's rotation with the camera's rotation
        transform.rotation = playerCamera.transform.rotation;
    }
}

