using System;
using System.Collections;
using System.Numerics;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject bulletPrefab;//public --> can be accessed from outside of this script, gameobject is the type of variable, last word is the name of the variable

    public bool isActiveWeapon;//public --> can be accessed from outside of this script, variable type is a boolean（true or false, last word is the name of the variable.
    public bool isShooting, readyToShoot;//public --> can be accessed from outside of this script, the type of variable is a boolean and there are 2 declared here.
    bool allowReset = true; //declaring a boolean variable and setting it to true at the start.
    public float shootingDelay = 2f;//public --> can be accessed from outside of this script, float is the variable type, so any number is fine, last word is the name of the variable, set to a default value of 2 seconds but can be changed in the editor window 
    public int bulletsPerBurst = 3;//public --> can be accessed from outside of this script,  the type of variable is integer so whole numbers, the last word is the name of the variable
    public int burstBulletsLeft;//public --> can be accessed from outside of this script, the type of variable is integer so whole numbers, the last word is the name of the variable
    public float spreadIntensity;//public --> can be accessed from outside of this script, //public --> can be accessed from outside of this script, float is the variable type, so any number is fine, last word is the name of the variable,
    public GameObject muzzleEffect;//public --> can be accessed from outside of this script, gameobject is the type of variable, last word is the name of the variable
    internal Animator animator; //Access Modifier(internal): The internal access modifier means that this variable is accessible only within the same assembly(i.e., within the same project or module). It's not accessible from other assemblies (like a different plugin or library). This is more restrictive than public but less restrictive than private. The type is the animator and its given a name of animator in this script
    public float reloadTime;//public --> can be accessed from outside of this script, //public --> can be accessed from outside of this script, float is the variable type, so any number is fine, last word is the name of the variable,
    public int magazineSize, bulletsLeft;//public --> can be accessed from outside of this script, the type of variable is integer(whole numbers), and there are 2 variables here declared
    public bool isReloading;//public --> can be accessed from outside of this script, variable type is a boolean（true or false, last word is the name of the variable.
    public UnityEngine.Vector3 spawnPosition;//public --> can be accessed from outside of this script, type of variable is a UnityEngine.Vector3, name is spawnposition
    public UnityEngine.Vector3 spawnRotation;//public --> can be accessed from outside of this script, type of variable is a UnityEngine.Vector3, name is spawnrotation
    public enum WeaponModel //public --> can be accessed from outside of this script, this is basically a list of data of the type weaponModel
    {
        pistols,
        RifleAK47,
        Snipers,

        Crackshots,
    }

    public LevelUiManager li; //declaring a public variable in the inspector
    public enum ShootingMode//public --> can be accessed from outside of this script, this is basically a list of data of the type shootingmode
    {
        Single,
        Burst,
        Auto,
    }
    public Transform bulletSpawn;//public --> can be accessed from outside of this script, variable type is the transform component of the gameobject, name is bulletspawn
    public float bulletVelocity = 30f;//public --> can be accessed from outside of this script, //public --> can be accessed from outside of this script, float is the variable type, so any number is fine, last word is the name of the variable, set to 30 on default
    public float bulletPrefabLifeTime = 3f;//public --> can be accessed from outside of this script,//public --> can be accessed from outside of this script, float is the variable type, so any number is fine, last word is the name of the variable, set to 3seconds on default

    public ShootingMode currentShootingMode;//public --> can be accessed from outside of this script, This variable stores the current shooting mode of the weapon. For example, it might be set to "SingleShot" for semi-automatic firing or "Automatic" for continuous firing.
    public WeaponModel thisWeaponModel;//public --> can be accessed from outside of this script, This variable stores the reference to the weapon model being used, which could include the weapon's appearance, stats, and attributes.

    private void Awake() //Awake: Sets initial values for the weapon's state and retrieves the Animator component.
    {
        readyToShoot = true; // the gun is ready to shoot by default
        burstBulletsLeft = bulletsPerBurst;  //sets the burstBulletsLeft to be its default size(bulletsPerburst)
        animator = GetComponent<Animator>(); //gets the information from the animator component

    }
    void Update()
    {


        if (isActiveWeapon && !li.isPaused) //if the weapon is active and the game isn't paused, this accesses the Variable isPaused in the level ui manager
        {
            GetComponent<Outline>().enabled = false; //// Hides the outline if the weapon is active

            if (bulletsLeft == 0 && isShooting)
            {

                ShootingSoundManager.Instance.emptySoundEffect.Play(); //// Plays empty gun soundeffect when there are no bullets left in the magazine but the player is still shooting
            }
            if (currentShootingMode == ShootingMode.Auto) //if the shooting mode is auto then the player can just shoot by pressing down left mouse key 
            {
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }
            else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst) //if it's single shot or burst shot, then the player has to click once for every shot
            {
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
            }
            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloading == false)
            {
                Reload(); //if R is pressed when bullets left is lower than magazineSize and the gun isn't reloading at this moment, then the gun reloads
            }
            //automatic reload when magazine empty
            if (readyToShoot && !isShooting && isReloading == false && bulletsLeft <= 0)
            {
                Reload(); //when the player can shoot but has nno bullet in its magazine the gun automatically reloads
            }
            if (readyToShoot && isShooting && bulletsLeft > 0) //if the player can shoot and is shooting and has bullets in its magazine,  then the burstbulletleft is set to its default size and the weapon is fired
            {
                burstBulletsLeft = bulletsPerBurst;
                FireWeapon();
            }
            if (AmmoManager.Instance.ammoDisplay != null) //Checks if there is an ammoDisplay text element in the AmmoManager singleton
            {
                AmmoManager.Instance.ammoDisplay.text = $"{bulletsLeft / bulletsPerBurst}/{magazineSize / bulletsPerBurst}"; //Updates the ammo display text to show the number of bullets left divided by the number of bullets per burst and the total magazine capacity divided by the bullets per burst. This helps the player see how many bursts are left in the current magazine.
            }
        }
    }
    private void FireWeapon()
    {
        bulletsLeft--; //decreases ammo count
        muzzleEffect.GetComponent<ParticleSystem>().Play(); //plays a muzzle effect
        animator.SetTrigger("Recoil"); //then enables a recoil animation

        ShootingSoundManager.Instance.PlayShootingSound(thisWeaponModel); //then plays the shooting sound

        readyToShoot = false; //the player temporarily cannot shoot
        UnityEngine.Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;// Calculates shooting direction with spread intensity

        //creates the bullet(instantiate) from the bulletspwan position set in the Unity editor,UnityEngine.Quaternion.identity means no rotation in any direction
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, UnityEngine.Quaternion.identity);
        //shoots the bullet forward in the direction calculated with spreadintensity
        bullet.transform.forward = shootingDirection;

        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse); //applies a force to the bullet, in the shooting direction and the speed of bullet velocity in a force mode of impulse(how the force is applied)
        //destroys the bullet after some time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime)); //starts a countdown to destroy the bullet after 3 seconds(the value of bulletPrefablifetime)

        if (allowReset)  //if the gun is allowed to reset
        {
            Invoke("ResetShot", shootingDelay); //// Resets the weapon after a delay
            allowReset = false; //cannot reset now so doesn't call the invoke method again(provided its not shooting)
        }

        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1) //if in burst mode and there are more than 1 burst bullets, 
        {
            burstBulletsLeft--; //decreases burst bullet by 1
            Invoke("FireWeapon", shootingDelay); //invokes the fireweapon method with a delay of 'shootingDelay'
        }


    }

    private void Reload()
    {
        //plays the sound effect specific to the weapon model
        ShootingSoundManager.Instance.PlayReloadSound(thisWeaponModel);
        animator.SetTrigger("Reload"); //triggers the reload triggerin the animator so the gun undergoes a reload animation
        isReloading = true;//the gun is reloading
        Invoke("reloadCompleted", reloadTime); //invoke the reload complete function with a delay of reloadTime
    }
    private void reloadCompleted()
    {
        bulletsLeft = magazineSize; //when reload is completed, the gun cannot reload and the bullets left = magazine size
        isReloading = false;
    }
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)// This IEnumerator method, DestroyBulletAfterTime, is used to destroy a bullet object after a specified delay('delay'). 
    {
        yield return new WaitForSeconds(delay); //The yield return statement pauses the execution of the coroutine. In this case, WaitForSeconds(delay) waits for a specified number of seconds (delay) before continuing. The delay parameter determines how long the coroutine will wait.
                                                //this bascially means that the bullet has to fly for a little distance, so even if the life time of the bullet is set to 0 the bullet will still fly for a bit which allows it to interact with other objects before being removed from the game
        Destroy(bullet); //destroys the bullet

    } //
    public UnityEngine.Vector3 CalculateDirectionAndSpread() //public --> can be accessed from outside of this script, the type of function is a vector 3 so it returns a value in the form of a vector 3
    {
        Ray ray = Camera.main.ViewportPointToRay(new UnityEngine.Vector3(0.5f, 0.5f, 0)); // Create a ray from the center of the screen (viewport point (0.5, 0.5)) in the direction the camera is facing
        RaycastHit hit; //stores the information of the ray(if it hit anything) in the 'hit'

        UnityEngine.Vector3 targetpoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetpoint = hit.point; //If the ray hits an object, hit.point is the location where the ray intersects that object, and targetpoint is set to this location.
        }
        else
        {
            targetpoint = ray.GetPoint(100); //If the ray does not hit any object, targetpoint is set to a point 100 units away from the ray's origin along the ray's direction. This ensures targetpoint is always set to a valid point even if nothing is hit.
        }
        UnityEngine.Vector3 direction = targetpoint - bulletSpawn.position; //This calculates the direction vector from the bullet spawn position to the targetpoint. This is the primary direction in which the projectile should be fired.
        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        return direction + new UnityEngine.Vector3(x, y, 0);
        //This introduces randomness to simulate the spread of the shot. x and y values are randomly selected within a specified range (spreadIntensity), adding variation to the direction.
        //The final return value is the direction vector adjusted by the random spread in the X and Y directions.This creates a more realistic firing pattern where bullets don’t always travel in a perfectly straight line.

    }
    private void ResetShot() //when the shot is reset, the player can shoot and it sets the allowReset back to true, enabling the weapon to be ready for another reset.
    {
        readyToShoot = true;
        allowReset = true;
    }

}
