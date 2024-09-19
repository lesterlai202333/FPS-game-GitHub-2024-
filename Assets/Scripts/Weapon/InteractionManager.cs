using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{

    public static InteractionManager Instance { get; set; }// by making it static it basically clamps the instance variable declared to this one script only. And in this singleton pattern unity makes sure only one InteractionManager script exists. 'get' allows you to retrieve or access the value of the property. 'set' allows you to assign or modify the value of the property.
    public Weapon hoveredWeapon = null;//public --> can be accessed from outside of this script, the type of variable is the weapon script, this variable holds a reference to a Weapon object, which is currently being hovered over, it's initiallized to null so no weapon is being hovered over at the start.
    private void Awake() //private means cannot be accessed elsewhere, Awake is a Unity method that is called when the script instance is being loaded, before the Start method and before the scene is fully initialized.
    {
        if (Instance != null && Instance != this) //This checks if there is already an instance of this class (represented by Instance) in existence.
        { //if yes, the gameObject with this script would be destroyed
            Destroy(gameObject);
        }
        else
        {
            Instance = this; //sets the instance variable to this one script
        }
    }
    //this is a singleton system, it's here to make sure that there's only one object in game that has this script, otherwise it can cause a lot of chaos and become mixed up with other things. 
    //So as the script awakes, it asks: are there any other gameobjects with this script? if yes, then that game object gets destroyed, if no, this would become the only gameobject with this script.
    private void Update()
    {
        // Create a ray from the center of the screen (viewport point (0.5, 0.5)) in the direction the camera is facing
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit; // Variable to store information about what the raycast hits

        // Cast a ray with a maximum distance of 1.5 units
        if (Physics.Raycast(ray, out hit, 1.5f))
        {
            // Get the GameObject that was hit by the raycast
            GameObject objecthitByRaycast = hit.transform.gameObject;

            // Check if the hit object has a Weapon component and if that weapon is inactive
            if (objecthitByRaycast.GetComponent<Weapon>() && !objecthitByRaycast.GetComponent<Weapon>().isActiveWeapon)
            {
                // Check if the currently hovered weapon is not the same as the newly hit weapon
                if (hoveredWeapon != objecthitByRaycast.GetComponent<Weapon>())
                {
                    // If there was a previously hovered weapon, disable its outline effect
                    if (hoveredWeapon != null)
                    {
                        hoveredWeapon.GetComponent<Outline>().enabled = false;
                    }

                    // Update the hovered weapon to the newly hit weapon
                    hoveredWeapon = objecthitByRaycast.GetComponent<Weapon>();
                    hoveredWeapon.GetComponent<Outline>().enabled = true; // Enable the outline effect for the new hovered weapon
                }

                // Check if the E key is pressed to pick up the weapon
                if (Input.GetKeyDown(KeyCode.E))
                {
                    WeaponManager.Instance.PickupWeapon(objecthitByRaycast.gameObject); // Call the pickup function from WeaponManager
                }
            }
            else
            {
                // If the hit object is not a weapon or is an active weapon
                if (hoveredWeapon != null)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false; // Disable the outline effect of the previously hovered weapon
                    hoveredWeapon = null; // Clear the reference to the previously hovered weapon
                }
            }
        }
        else
        {
            // If no object was hit by the raycast
            if (hoveredWeapon != null)
            {
                hoveredWeapon.GetComponent<Outline>().enabled = false; // Disable the outline effect of the previously hovered weapon
                hoveredWeapon = null; // Clear the reference to the previously hovered weapon
            }
        }
    }


}
