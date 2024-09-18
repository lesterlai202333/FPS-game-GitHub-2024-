using UnityEngine;

public abstract class Interactable : MonoBehaviour //Interactable is declared as abstract so it cannot be instantiated directly. Instead, it is intended to be used as a base class for other classes that inherit from it.
{
    public bool useEvents;//public --> can be accessed from outside of this script, variable type is a boolean（true or false, last word is the name of the variable. This boolean field determines whether the interactable object should trigger an event when interacted with.
    public string promptMessage;//public --> can be accessed from outside of this script, type of variable is a string(text), it's named prompttext. This string field holds a message that can be displayed to the player
    public void BaseInteract() //public --> can be accessed from outside of this script
    {
        if (useEvents)//checks if useEvents is true. If so, it triggers an event using InteractionEvent’s OnInteract delegate.

            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
        //Interactable relies on having an InteractionEvent component attached to the same GameObject. When BaseInteract() is called, it checks if useEvents is true and then uses GetComponent<InteractionEvent>() to find the InteractionEvent component. Event Invocation: If the InteractionEvent component is found, it invokes the OnInteract event, which can have various methods assigned to it via the Unity Inspector. This allows for customizable interactions in derived classes or through Unity's event system.
    }
    protected virtual void Interact()//This is a virtual method that can be overridden by derived classes. It is intended to be used to define specific interaction behavior that is unique to the particular type of interactable object.
    {

    }
}
