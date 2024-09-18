using UnityEngine;

public class GunFollow : MonoBehaviour

{
    public Camera playerCamera;   // Assign the player's camera in the Inspector, public --> can be accessed from outside of this script
    public Vector3 gunOffset;   // Offset to keep the gun in a fixed position relative to the camera, //public --> can be accessed from outside of this script
    void Start()
    {
        transform.position = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y - 0.31f, playerCamera.transform.position.z + 0.02f) + playerCamera.transform.TransformDirection(gunOffset);
        //sets the position of the object to be slightly below and in front of the player's camera. The method playerCamera.transform.TransformDirection(gunOffset) is used to convert a direction vector from local space to world space. It basically just so it adjusts the direction of the gun to the same as the camera
        transform.rotation = playerCamera.transform.rotation; //sets the rotation of the object that has this script to the rotation of the camera(so they face the same direction)
    }

    // Update is called once per frame

    void Update()
    {
        // Update the gun's position and rotation every frame to follow the camera
        FollowCamera();
    }

    void FollowCamera()
    {
        // Set the gun's position to the camera's position plus the offset(so it's always in a fixed position relative to the camera(that's x=0, y=-0.31, z = 0.7 (as a child to the camera)))
        transform.position = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y - 0.31f, playerCamera.transform.position.z + 0.7f) + playerCamera.transform.TransformDirection(gunOffset);//again, alligns the direction of the gun to be the same as the camera

        // Align the gun's rotation with the camera's rotation
        transform.rotation = playerCamera.transform.rotation;
    }
}

