using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; set; }// by making it static it basically clamps the instance variable declared to this one script only. And in this singleton pattern unity makes sure only one WeaponManager script exists. 'get' allows you to retrieve or access the value of the property. 'set' allows you to assign or modify the value of the property.
    public List<GameObject> weaponSlots;//public --> can be accessed from outside of this script, this is a list that contains gameObjects, it will be visible in the unity editor
    public GameObject activeWeaponSlots;//public --> can be accessed from outside of this script, gameobject is the type of variable, last word is the name of the variable
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
    private void Start()
    {
        activeWeaponSlots = weaponSlots[0]; //active weapon slot is set to the first weaponslot(alpha 1)
    }

    private void Update()
    {
        foreach (GameObject weaponSlot in weaponSlots)
        {
            if (weaponSlot == activeWeaponSlots) //Iterates through all weapon slots(weaponSlots), checking if each slot is the currently active one(activeWeaponSlots).
            {
                weaponSlot.SetActive(true); // if it is, then set the weapon slot active in the game scene
            }
            else
            {
                weaponSlot.SetActive(false); //else deactivates this weaponslot in the game
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchActiveSlot(0); //pressing 1 gives the first weapon slot
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchActiveSlot(1); //pressing 2 gives the second slot
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchActiveSlot(2); //pressing 3 gives the 3rd slot
        }


    }
    public void PickupWeapon(GameObject pickedupWeapon) //this method takes in a parameter of type gameobject(pickedupweapon)
    {
        AddWeaponIntoActiveSlot(pickedupWeapon);  //Calls AddWeaponIntoActiveSlot to place the newly picked-up weapon into the currently active weapon slot.
    }

    public void AddWeaponIntoActiveSlot(GameObject pickedupWeapon)//public --> can be accessed from outside of this script, this method takes in a parameter of type gameobject(pickedupweapon)
    {
        DropCurrentWeapon(pickedupWeapon);
        pickedupWeapon.transform.SetParent(activeWeaponSlots.transform, false); //set the picked up weapon as a child of the active weapon slot

        Weapon weapon = pickedupWeapon.GetComponent<Weapon>(); //Access the Weapon component from the picked-up weapon

        pickedupWeapon.transform.localPosition = new Vector3(weapon.spawnPosition.x, weapon.spawnPosition.y, weapon.spawnPosition.z); //sets the local position of the weapon to a set of transform positions stored in the weapon component
        pickedupWeapon.transform.localRotation = Quaternion.Euler(weapon.spawnRotation.x, weapon.spawnRotation.y, weapon.spawnRotation.z); ////sets the local Rotation of the weapon to a set of transform positions stored in the weapon component
        weapon.isActiveWeapon = true; //makes this weapon the active weapon
        weapon.animator.enabled = true; //enables the weapon's animator
    }
    public void DropCurrentWeapon(GameObject pickedupWeapon)//public --> can be accessed from outside of this script
    {
        if (activeWeaponSlots.transform.childCount > 0) //// Check if there’s a weapon currently in the active slot
        {
            var weaponToDrop = activeWeaponSlots.transform.GetChild(0).gameObject; //the weapon to be dropped is the weapon in the active weapon slot (var lets you avoid explicitly typing out GameObject. The compiler infers that weaponToDrop is of type GameObject because activeWeaponSlots.transform.GetChild(0).gameObject returns a GameObject.)
            weaponToDrop.GetComponent<Weapon>().isActiveWeapon = false; //the weapon component of the weapon is now inactive
            weaponToDrop.GetComponent<Weapon>().animator.enabled = false; //animator of the weapon is also now inactive
            weaponToDrop.transform.SetParent(pickedupWeapon.transform.parent); // the weapon in the activeweaponslot simply go into the hierachy because the picked up weapon(that's not a child of any object in game) is a child of the hierachy. 
            weaponToDrop.transform.localPosition = pickedupWeapon.transform.localPosition; //when the current weapon is dropped, it match the position of the picked-up weapon
            weaponToDrop.transform.localRotation = pickedupWeapon.transform.localRotation;//when the current weapon is dropped, it match the rotation of the picked-up weapon so the weapon that's dropped would sit on the same spot as the weapon picked up and rotate the same way

        }
    }

    public void SwitchActiveSlot(int slotNumber)//public --> can be accessed from outside of this script
    {
        // Step 1: Deactivate the current active weapon (if any)
        if (activeWeaponSlots != null && activeWeaponSlots.transform.childCount > 0) //if this is the active weapon slot and there is a weapon in this slot
        {
            Weapon currentWeapon = activeWeaponSlots.transform.GetChild(0).GetComponent<Weapon>(); //accesses the weapon component of the weaon in the active slot
            if (currentWeapon != null) //if there is a current weapon
            {
                currentWeapon.isActiveWeapon = false; // Deactivate current weapon
                currentWeapon.gameObject.SetActive(false); // Hide the current weapon
            }
        }

        // Switch to another weapon slot
        activeWeaponSlots = weaponSlots[slotNumber];

        // Step 3: Ensure the new weapon (if any) is activated
        if (activeWeaponSlots != null && activeWeaponSlots.transform.childCount > 0) //if there is an active weapon slot and there is a weapon in this slot
        {
            Weapon newWeapon = activeWeaponSlots.transform.GetChild(0).GetComponent<Weapon>(); //accesses the weaponcomponent of this weapon that's newly added in to the active weapon slot
            if (newWeapon != null) //if there is a new weapon
            {
                newWeapon.isActiveWeapon = true; // Activate new weapon
                newWeapon.gameObject.SetActive(true); // Show the new weapon
            }

        }

    }
}
