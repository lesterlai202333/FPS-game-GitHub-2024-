using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{

    public float health; //public --> can be accessed from outside of this script, float is the variable type, so any number is fine, last word is the name of the variable
    public GameObject character;//public --> can be accessed from outside of this script, gameobject is the type of variable, last word is the name of the variable
    public float deathTimer = 3f;//public --> can be accessed from outside of this script, float is the variable type, so any number is fine, last word is the name of the variable
    public bool dead;//public --> can be accessed from outside of this script, variable type is a boolean（true or false, last word is the name of the variable.

    public float deathStartTime;//public --> can be accessed from outside of this script, float is the variable type, so any number is fine, last word is the name of the variable
    public GameObject deathScreen;//public --> can be accessed from outside of this script, gameobject is the type of variable, last word is the name of the variable
    private float lerpTimer; //privte means cannot be accessed outside of this script. float is the variable type, so any number is fine, last word is the name of the variable
    [Header("Health Bar")] //creates a header line in the unity editor to make things easier
    public float maxHealth = 100f;//public --> can be accessed from outside of this script, float is the variable type, so any number is fine, last word is the name of the variable, set to a default value of 100
    public float chipSpeed = 2f;//public --> can be accessed from outside of this script, float is the variable type, so any number is fine, last word is the name of the variable, set to a default value of 2   
    public Image frontHealthBar;//public --> can be accessed from outside of this script, image is the type of variable, last word is name of variable    

    public Image backHealthBar;//public --> can be accessed from outside of this script, image is the type of variable, last word is name of variable

    [Header("Damage Overlay")]//creates a header line in the unity editor to make things easier
    public Image overlay;//public --> can be accessed from outside of this script
    public float duration;//public --> can be accessed from outside of this script, float is the variable type, so any number is fine, last word is the name of the variable
    public float fadeSpeed;//public --> can be accessed from outside of this script, float is the variable type, so any number is fine, last word is the name of the variable
    private float durationTimer;//privte means cannot be accessed outside of this script. float is the variable type, so any number is fine, last word is the name of the variable
    [Header("Heal Overlay")]//creates a header line in the unity editor to make things easier
    public Image overlayH;//public --> can be accessed from outside of this script, image is the type of variable, last word is name of variable
    public float durationH;//public --> can be accessed from outside of this script, float is the variable type, so any number is fine, last word is the name of the variable
    public float fadeSpeedH;//public --> can be accessed from outside of this script, float is the variable type, so any number is fine, last word is the name of the variable
    private float durationTimerH;//privte means cannot be accessed outside of this script. float is the variable type, so any number is fine, last word is the name of the variable



    void Awake()
    {
        health = maxHealth; //setting the health to max health

        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        overlayH.color = new Color(overlayH.color.r, overlayH.color.g, overlayH.color.b, 0); //the color of both overlays are set to the exact same as how they are, however their alpha is set to 0 so they are fully transparent and don't show up on the screen
        deathScreen.SetActive(false); //deactivates the deathScreen
        deathTimer = 3f; //deathtimer set to 3f on default
        dead = false; //player isn't dead
        Time.timeScale = 1f; //time runs normally

    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, -100, maxHealth); //clamping the health within the range of -100 and maxhealth
        UpdateHealthUI();
        DamageOverlayCheck();
        HealthOverlayCheck();
        death(); //constantly calling these 4 functions
    }

    public void UpdateHealthUI()//public --> can be accessed from outside of this script
    {

        float fillFrontHealthBar = frontHealthBar.fillAmount;
        float fillBackHealthBar = backHealthBar.fillAmount;
        float healthFraction = health / maxHealth;
        if (fillBackHealthBar > healthFraction)
        {
            frontHealthBar.fillAmount = healthFraction; //The front health bar is constantly set to the current health (frontHealthBar.fillAmount = healthFraction), reflecting the actual health value in the form of fillamount.
            backHealthBar.color = Color.red; //damage is red
            lerpTimer += Time.deltaTime; //lerpTimer is incremented using Time.deltaTime, which measures the time between frames.so the interpolation is time-based.
            float percentComplete = lerpTimer / chipSpeed; //The interpolation percentage (percentComplete) is calculated by dividing lerpTimer by chipSpeed. This value is squared (percentComplete * percentComplete) to make the animation more gradual at the beginning and speed up toward the end.
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBackHealthBar, healthFraction, percentComplete);// The back health bar(which represents the health lost) is gradually decreased using linear interpolation (Mathf.Lerp), creating a smooth "health loss" animation.
        }
        if (fillFrontHealthBar < healthFraction) //This block is executed when the front health bar is less than the healthFraction. This happens when the player gains health.
        {
            backHealthBar.fillAmount = healthFraction; // back health bar is instantly set to the current health (backHealthBar.fillAmount = healthFraction), reflecting the updated maximum health after healing.
            backHealthBar.color = Color.green; //healing is green
            lerpTimer += Time.deltaTime; //lerpTimer is incremented using Time.deltaTime, which measures the time between frames.so the interpolation is time-based.
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete; //The interpolation percentage(percentComplete) is calculated using the lerpTimer and chipSpeed, then squared to control the smoothness of the transition.
            frontHealthBar.fillAmount = Mathf.Lerp(fillFrontHealthBar, backHealthBar.fillAmount, percentComplete); //The front health bar is gradually increased using linear interpolation, creating a smooth "health gain" animation.
        }
        //When the player takes damage, the front health bar instantly reflects the new health, while the back bar, colored red, gradually decreases. When the player heals, the back bar is immediately set to the new health, and the front bar, colored green, smoothly increases to match. This is achieved through time-based interpolation using Mathf.Lerp for smooth animations during health changes.
    }
    public void TakeDamage(float damage)//public --> can be accessed from outside of this script， void means no values returned, this function specifies a parameter called damageamount. So when I call this function I can put a number inside the brackets e.g TakeDamage(20) and it would damage the player by 20hp. It makes the code much more clean and flexible
    {
        health -= damage; //health decreases by the amount of damage taken. 
        lerpTimer = 0f; //timer set to 0, This ensures that each time damage is taken or health is restored, the transition starts afresh. Without resetting lerpTimer, the interpolation would continue from its previous state, causing jerky visual updates on the health bars.
        durationTimer = 0; //timer set to 0
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1); //the alpha is set to 1 so the overlay shows on the screen

    }
    public void RestoreHealth(float healAmount)//public --> can be accessed from outside of this script， void means no values returned, this function specifies a parameter called healamount. So when I call this function I can put a number inside the brackets e.g RestoreHealth(20) and it would heal the player by 20hp. It makes the code much more clean and flexible
    {
        health += healAmount; //health increases by healamount
        lerpTimer = 0f; //timer set to 0, This ensures that each time damage is taken or health is restored, the transition starts afresh. Without resetting lerpTimer, the interpolation would continue from its previous state, causing jerky visual updates on the health bars.
        durationTimerH = 0; //timer set to 0
        overlayH.color = new Color(overlayH.color.r, overlayH.color.g, overlayH.color.b, 1); ////the alpha is set to 1 so the overlay shows on the screen

    }

    public void HealthOverlayCheck()//public --> can be accessed from outside of this script, void means no values returned, 
    {
        if (overlayH.color.a > 0) //this line here checks whether the overlay is visible(only if it is visble it would proceed)
        {

            if (health < 80) //the health overlay only shows if the health is below 80
            {
                durationTimerH += Time.deltaTime; //so this is a type of timer, as the durationtimer is set to 0 when the player takes damage, it would now increase and when it equals the durationlength we set, it triggers the stuff in the if function
                if (durationTimerH > durationH)
                { //the time overlay now starts fading out, the temp alpha is gradually decreased to 0 and used as the alpha value so the transparency decreases overtime
                    float tempAlphaH = overlayH.color.a;
                    tempAlphaH -= Time.deltaTime * fadeSpeedH;
                    overlayH.color = new Color(overlayH.color.r, overlayH.color.g, overlayH.color.b, tempAlphaH);
                }
            }
            else
            {
                overlayH.color = new Color(overlayH.color.r, overlayH.color.g, overlayH.color.b, 0); //other wise it's just transparent
                durationTimerH = 0; //duration timer is reset to 0
            }

        }
    }

    public void DamageOverlayCheck()//public --> can be accessed from outside of this script,  doesn't return any values
    {
        if (overlay.color.a > 0) //is the damage overlay visible? if yes, then proceeds
        {
            if (health < 30)
                return; //if health is very low then the function exits early so the overlay just stays on
            durationTimer += Time.deltaTime; //the durationTimer just keeps addingup
            if (durationTimer > duration)
            { //if the timer exceeds the duration and health > 30, the overlay would be fading away similarly to the healthoverlay
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }
    public void death()//public --> can be accessed from outside of this script, doesn't return any values
    {
        if (health <= 0 && !dead) //if health is <= 0 and the player isn't dead before, the player can die else it dies twice
        {
            deathScreen.SetActive(true);//deathscreen active
            dead = true; //sets the bool to true
            deathStartTime = Time.realtimeSinceStartup; // Store the time when the player dies
            Time.timeScale = 0; // Pause the game
        }

        if (dead == true) //if the player is dead
        {
            float elapsedTime = Time.realtimeSinceStartup - deathStartTime;//starts tracking how long it is since the player died

            if (elapsedTime >= 3f && Input.anyKeyDown) //if it's more than 3 seconds and the player pressed anykey, the player gets to restart the level
            {
                Time.timeScale = 1f; // Restore the time scale before reloading
                SceneManager.LoadScene("Scene 1"); //loads the scene again.
            }

        }

    }
}
