using UnityEngine;
using UnityEngine.InputSystem;



public class Playerinteract : MonoBehaviour
{
    private Camera cam;//private means cannot be accessed outside of this script, variable type is a camera and its given a name of cam
    private float distance = 3f; //private means cannot be accessed outside of this script, float is the variable type, so any number is fine, last word is the name of the variable
    [SerializeField] private LayerMask mask; // This is a Unity-specific attribute that allows private fields to be visible and editable in the Unity Inspector. Normally, private fields wouldn’t show up in the Unity Editor, but using [SerializeField] means you can still modify them in the Inspector without changing their access level.
    private PlayerUI playerUI; //private means cannot be accessedd outside of this script, the variable declared is the playerUI script, and it's name is given as playerUI
    private InputManager iM; //private means cannot be accessedd outside of this script, the type of variable declared is the InputManager, it's given a name of IM in the script.





    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        iM = GetComponent<InputManager>(); //basically accessing the  information in the components of these variables.

    }




    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty); //constantly getting rid of the text 
        Ray ray = new Ray(cam.transform.position, cam.transform.forward); //Constructs a Ray starting from the camera's position and pointing forward.
        Debug.DrawRay(ray.origin, ray.direction * distance); // Visualizes the ray in the Scene view, helping with debugging.
        RaycastHit hitInfo; //information stored from the raycast 
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null) //if the object hit by the ray has an Interactable component
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>(); //The Interactable component(if found) is assigned to the variable interactable.
                playerUI.UpdateText(interactable.promptMessage); //the script updates the UI to show the interaction prompt message.


                if (iM.onFoot.Interact.triggered) //Checks if the interact button is pressed (using iM.onFoot.Interact.triggered), and if so, it calls the BaseInteract method on the interactable object.
                {
                    interactable.BaseInteract();
                }


            }
        }

    }



}












