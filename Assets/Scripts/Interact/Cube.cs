using UnityEngine;

public class Cube : Interactable
{
    [SerializeField] private GameObject door;
    private bool doorOpen;
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorCloseSound;
    [SerializeField] private AudioSource doorAudioSource;  // declaring audiosources and audio clips to be assigned in the unity editor window
    protected override void Interact()
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
