using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject door;
    private bool doorOpen;
    public string promptMessage;
    public void BaseInteract()
    {
        Interact();
    }
    protected virtual void Interact()
    {
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
    }
}
