using UnityEngine;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText; //// This is a Unity-specific attribute that allows private fields to be visible and editable in the Unity Inspector. Normally, private fields wouldn’t show up in the Unity Editor, but using [SerializeField] means you can still modify them in the Inspector without changing their access level. private means cannot be accessed in other scripts, declaring a TMP variable to be accessed below

    // Update is called once per frame
    public void UpdateText(string promptMessage) //public --> can be accessed from outside of this script, void means doesn't give back any values(only performs an action), inside the bracket(string promptMessage) represents the parameters that this function accepts, 'string' is the type of data, and promptmessage is the name of the parameter （remember! The promptMessage was defined in the interactable script.)
    {
        promptText.text = promptMessage; //promptText.text accessed the text component of TMP in unity editor(accesses the actual text displayed), 
    } //the parameter promptMessage is set to be equal to the text in the TMP 
}
