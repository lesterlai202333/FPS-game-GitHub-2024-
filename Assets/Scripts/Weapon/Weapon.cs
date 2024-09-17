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
    public GameObject bulletPrefab;

    public bool isActiveWeapon;
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;
    public float spreadIntensity;
    public GameObject muzzleEffect;
    internal Animator animator;
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;
    public UnityEngine.Vector3 spawnPosition;
    public UnityEngine.Vector3 spawnRotation;
    public enum WeaponModel
    {
        pistols,
        RifleAK47,
        Snipers,

        Crackshots,
    }


    public enum ShootingMode
    {
        Single,
        Burst,
        Auto,
    }
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletPrefabLifeTime = 3f;

    public ShootingMode currentShootingMode;
    public WeaponModel thisWeaponModel;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();
    }
    void Update()
    {


        if (isActiveWeapon)
        {
            GetComponent<Outline>().enabled = false;

            if (bulletsLeft == 0 && isShooting)
            {
                //ShootingSoundManager.Instance.PistolemptySoundEffect.Play();
                ShootingSoundManager.Instance.emptySoundEffect.Play();
            }
            if (currentShootingMode == ShootingMode.Auto)
            {
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }
            else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
            {
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
            }
            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloading == false)
            {
                Reload();
            }
            //automatic reload when magazine empty
            if (readyToShoot && !isShooting && isReloading == false && bulletsLeft <= 0)
            {
                Reload();
            }
            if (readyToShoot && isShooting && bulletsLeft > 0)
            {
                burstBulletsLeft = bulletsPerBurst;
                FireWeapon();
            }
            if (AmmoManager.Instance.ammoDisplay != null)
            {
                AmmoManager.Instance.ammoDisplay.text = $"{bulletsLeft / bulletsPerBurst}/{magazineSize / bulletsPerBurst}";
            }
        }
    }
    private void FireWeapon()
    {
        bulletsLeft--;
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("Recoil");
        //ShootingSoundManager.Instance.pistolshootingSound.Play();
        ShootingSoundManager.Instance.PlayShootingSound(thisWeaponModel);

        readyToShoot = false;
        UnityEngine.Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;
        //creates the bullet(instantiate)


        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, UnityEngine.Quaternion.identity);
        //shoots the bullet forward

        bullet.transform.forward = shootingDirection;

        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        //destroys the bullet after some time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }


    }

    private void Reload()
    {
        //ShootingSoundManager.Instance.PistolreloadingSoundEffect.Play();
        ShootingSoundManager.Instance.PlayReloadSound(thisWeaponModel);
        animator.SetTrigger("Reload");
        isReloading = true;
        Invoke("reloadCompleted", reloadTime);
    }
    private void reloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
    }
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);

    }
    public UnityEngine.Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new UnityEngine.Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        UnityEngine.Vector3 targetpoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetpoint = hit.point;
        }
        else
        {
            targetpoint = ray.GetPoint(100);
        }
        UnityEngine.Vector3 direction = targetpoint - bulletSpawn.position;
        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        return direction + new UnityEngine.Vector3(x, y, 0);

    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

}
