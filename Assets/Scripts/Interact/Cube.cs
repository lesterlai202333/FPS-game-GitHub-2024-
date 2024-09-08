using UnityEngine;

public class Cube : Interactable
{
    [SerializeField] private GameObject door;
    private bool doorOpen;
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorCloseSound;
    [SerializeField] private AudioSource doorAudioSource;  // The AudioSource component attached to the door
    protected override void Interact()
    {
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
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
