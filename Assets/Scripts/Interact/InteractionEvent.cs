using UnityEngine;
using UnityEngine.Events;

public class InteractionEvent : MonoBehaviour
{
    public UnityEvent OnInteract; //public --> can be accessed from outside of this script, Manages events related to interactions. It holds a UnityEvent called OnInteract. 
                                  //UnityEvent allows you to assign and invoke multiple methods from the Unity Inspector. It's often used for triggering actions in response to certain events.
}
//Interactable relies on having an InteractionEvent component attached to the same GameObject. When BaseInteract() is called, it checks if useEvents is true and then uses GetComponent<InteractionEvent>() to find the InteractionEvent component.
//Event Invocation: If the InteractionEvent component is found, it invokes the OnInteract event, which can have various methods assigned to it via the Unity Inspector. This allows for customizable interactions in derived classes or through Unity's event system.