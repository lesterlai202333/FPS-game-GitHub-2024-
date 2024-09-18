using UnityEngine;

public class Cube : Interactable // indicates that Cube is a subclass of Interactable. This means Cube inherits the properties and methods of the Interactable class, which allows Cube to have all the functionality of Interactable, plus any additional functionality you define in Cube.
{
    [SerializeField] private GameObject door;// This is a Unity-specific attribute that allows private fields to be visible and editable in the Unity Inspector. Normally, private fields wouldn’t show up in the Unity Editor, but using [SerializeField] means you can still modify them in the Inspector without changing their access level. private means cannot be accessed out of this script, gameobject is the type of variable, last word is the name of the variable
    private bool doorOpen; //private means cannot be accessed out of this script, variable type is a boolean（true or false, last word is the name of the variable.
    [SerializeField] private AudioClip doorOpenSound;// This is a Unity-specific attribute that allows private fields to be visible and editable in the Unity Inspector. Normally, private fields wouldn’t show up in the Unity Editor, but using [SerializeField] means you can still modify them in the Inspector without changing their access level. private means cannot be accessed out of this script, the variable type is an audioclip, and the last word is the name of the variable
    [SerializeField] private AudioClip doorCloseSound;// This is a Unity-specific attribute that allows private fields to be visible and editable in the Unity Inspector. Normally, private fields wouldn’t show up in the Unity Editor, but using [SerializeField] means you can still modify them in the Inspector without changing their access level.private means cannot be accessed out of this script,the variable type is an audioclip, and the last word is the name of the variable
    [SerializeField] private AudioSource doorAudioSource;  // This is a Unity-specific attribute that allows private fields to be visible and editable in the Unity Inspector. Normally, private fields wouldn’t show up in the Unity Editor, but using [SerializeField] means you can still modify them in the Inspector without changing their access level. declaring audiosources and audio clips to be assigned in the unity editor window, Private means it cannot be accessed in other scripts, the variable type is an audioclip, and the last word is the name of the variable
    protected override void Interact() //allows you to define specific behavior for interactions with a Cube, while still inheriting the general interaction capabilities from the Interactable base class.       so if I later have scripts that inherit from the cube script, the protected method would keep all of the functions and bring it into the inherited scripts。
    {
        doorOpen = !doorOpen; //This line toggles the boolean doorOpen between true and false. If doorOpen is currently true, it becomes false, and vice versa.
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen); //This line accesses the Animator component of the door object and sets the boolean parameter "IsOpen" to the current value of doorOpen. This will control the animation state (whether the door appears open or closed).
        if (doorOpen)
        {
            doorAudioSource.PlayOneShot(doorOpenSound);  // Play the door opening sound
        }
        else
        {
            doorAudioSource.PlayOneShot(doorCloseSound); // Play the door closing sound
        }
    }

}
