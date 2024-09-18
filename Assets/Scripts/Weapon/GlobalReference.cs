using UnityEngine;

public class GlobalReference : MonoBehaviour
{
    public static GlobalReference Instance { get; set; }// by making it static it basically clamps the instance variable declared to this one script only. And in this singleton pattern unity makes sure only one GlobalReference script exists. 'get' allows you to retrieve or access the value of the property. 'set' allows you to assign or modify the value of the property.
    public GameObject bulletImpactEffectPrefab; //public --> can be accessed from outside of this script, variable type is a gameobject, and last word is its name
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
