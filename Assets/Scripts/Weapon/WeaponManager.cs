using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; set; }
    public List<GameObject> weaponSlots;
    public GameObject activeWeaponSlots;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        activeWeaponSlots = weaponSlots[0];
    }

    private void Update()
    {
        foreach (GameObject weaponSlot in weaponSlots)
        {
            if (weaponSlot == activeWeaponSlots)
            {
                weaponSlot.SetActive(true);
            }
            else
            {
                weaponSlot.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchActiveSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchActiveSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchActiveSlot(2);
        }


    }
    public void PickupWeapon(GameObject pickedupWeapon)
    {
        AddWeaponIntoActiveSlot(pickedupWeapon);
    }

    public void AddWeaponIntoActiveSlot(GameObject pickedupWeapon)
    {
        DropCurrentWeapon(pickedupWeapon);
        pickedupWeapon.transform.SetParent(activeWeaponSlots.transform, false);

        Weapon weapon = pickedupWeapon.GetComponent<Weapon>();

        pickedupWeapon.transform.localPosition = new Vector3(weapon.spawnPosition.x, weapon.spawnPosition.y, weapon.spawnPosition.z);
        pickedupWeapon.transform.localRotation = Quaternion.Euler(weapon.spawnRotation.x, weapon.spawnRotation.y, weapon.spawnRotation.z);
        weapon.isActiveWeapon = true;
        weapon.animator.enabled = true;
    }
    public void DropCurrentWeapon(GameObject pickedupWeapon)
    {
        if (activeWeaponSlots.transform.childCount > 0)
        {
            var weaponToDrop = activeWeaponSlots.transform.GetChild(0).gameObject;
            weaponToDrop.GetComponent<Weapon>().isActiveWeapon = false;
            weaponToDrop.GetComponent<Weapon>().animator.enabled = false;
            weaponToDrop.transform.SetParent(pickedupWeapon.transform.parent);
            weaponToDrop.transform.localPosition = pickedupWeapon.transform.localPosition;
            weaponToDrop.transform.localRotation = pickedupWeapon.transform.localRotation;

        }
    }

    public void SwitchActiveSlot(int slotNumber)
    {
        // Step 1: Deactivate the current active weapon (if any)
        if (activeWeaponSlots != null && activeWeaponSlots.transform.childCount > 0)
        {
            Weapon currentWeapon = activeWeaponSlots.transform.GetChild(0).GetComponent<Weapon>();
            if (currentWeapon != null)
            {
                currentWeapon.isActiveWeapon = false; // Deactivate current weapon
                currentWeapon.gameObject.SetActive(false); // Hide the current weapon
            }
        }

        // Step 2: Switch to the new weapon slot
        activeWeaponSlots = weaponSlots[slotNumber];

        // Step 3: Ensure the new weapon (if any) is activated
        if (activeWeaponSlots != null && activeWeaponSlots.transform.childCount > 0)
        {
            Weapon newWeapon = activeWeaponSlots.transform.GetChild(0).GetComponent<Weapon>();
            if (newWeapon != null)
            {
                newWeapon.isActiveWeapon = true; // Activate new weapon
                newWeapon.gameObject.SetActive(true); // Show the new weapon
            }

        }

    }
}
