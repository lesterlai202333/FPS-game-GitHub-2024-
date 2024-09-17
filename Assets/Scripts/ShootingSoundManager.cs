using UnityEngine;
using static Weapon;
public class ShootingSoundManager : MonoBehaviour
{
    public static ShootingSoundManager Instance { get; set; }
    public AudioSource ShootingChannel;

    public AudioClip pistolShots;
    public AudioClip rifleShots;

    public AudioSource pistolReloadingSoundEffect;
    public AudioSource rifleReloadingSoundEffect;
    public AudioSource emptySoundEffect;
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

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.pistols:
                ShootingChannel.PlayOneShot(pistolShots);
                break;
            case WeaponModel.RifleAK47:
                ShootingChannel.PlayOneShot(rifleShots);
                break;
        }
    }
    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.pistols:
                pistolReloadingSoundEffect.Play();
                break;
            case WeaponModel.RifleAK47:
                rifleReloadingSoundEffect.Play();
                break;
        }
    }
}
