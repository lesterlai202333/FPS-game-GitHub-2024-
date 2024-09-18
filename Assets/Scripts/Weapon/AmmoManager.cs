using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{

    //UI
    public TextMeshProUGUI ammoDisplay;//public --> can be accessed from outside of this script, variable type is a TMP, name is give as the last word
    public static AmmoManager Instance { get; set; }// by making it static it basically clamps the instance variable declared to this one script only. And in this singleton pattern unity makes sure only one AmmoManager script exists. 'get' allows you to retrieve or access the value of the property. 'set' allows you to assign or modify the value of the property.

    //The Instance variable acts as a global access point to that particular class. Once an object sets itself as the Instance, other parts of the code can easily refer to it, without needing to create new objects of that class. For example, other scripts can quickly reference the singleton like this: GameManager.Instance.
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

}
