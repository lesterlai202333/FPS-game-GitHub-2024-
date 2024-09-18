using UnityEngine;
using static Weapon;
public class ShootingSoundManager : MonoBehaviour
{
    public static ShootingSoundManager Instance { get; set; }// by making it static it basically clamps the instance variable declared to this one script only. And in this singleton pattern unity makes sure only one ShootingSoundManager script exists. 'get' allows you to retrieve or access the value of the property. 'set' allows you to assign or modify the value of the property.
    public AudioSource ShootingChannel;//public --> can be accessed from outside of this script, this is the audiosource that plays the 2 audio clips

    public AudioClip pistolShots;//public --> can be accessed from outside of this script, type of variable is audioclip and its the pistolshootingsound
    public AudioClip rifleShots;//public --> can be accessed from outside of this script type of variable is audioclip and its the rifleshootingsound

    public AudioSource pistolReloadingSoundEffect;//public --> can be accessed from outside of this script, type of variable is audiosource, last word is its name
    public AudioSource rifleReloadingSoundEffect;//public --> can be accessed from outside of this script type of variable is audiosource, last word is its name
    public AudioSource emptySoundEffect;//public --> can be accessed from outside of this script
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

    public void PlayShootingSound(WeaponModel weapon)//: This method plays a shooting sound based on the type of weapon being used.

    {
        switch (weapon) // Switches based on the type of weapon provided
        {
            case WeaponModel.pistols: // if it's a pistol then play the pistol shooting sound
                ShootingChannel.PlayOneShot(pistolShots);
                break; //ends the current case
            case WeaponModel.RifleAK47: // if it's an AK47 then play the rifle shooting sound
                ShootingChannel.PlayOneShot(rifleShots);
                break;//ends the current case
        }
    }
    public void PlayReloadSound(WeaponModel weapon)//This method plays a reloading sound based on the type of weapon being used.
    {
        switch (weapon)// Switches based on the type of weapon provided
        {
            case WeaponModel.pistols: //if it's a pistol then play the pistol reloading sound
                pistolReloadingSoundEffect.Play();
                break;//ends the current case
            case WeaponModel.RifleAK47: //if it's an ak47 then play the rifle reload sound
                rifleReloadingSoundEffect.Play();
                break;//ends the current case
        }
    }
}
