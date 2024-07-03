using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    private float health;
    private float lerpTimer;
    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    [Header("Damage Overlay")]
    public Image overlay;
    public float duration;
    public float fadeSpeed;
    private float durationTimer;
    [Header("Heal Overlay")]
    public Image overlayH;
    public float durationH;
    public float fadeSpeedH;
    private float durationTimerH;



    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        overlayH.color = new Color(overlayH.color.r, overlayH.color.g, overlayH.color.b, 0);

    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        DamageOverlayCheck();
        HealthOverlayCheck();

    }
    public void UpdateHealthUI()
    {

        float fillFrontHealthBar = frontHealthBar.fillAmount;
        float fillBackHealthBar = backHealthBar.fillAmount;
        float healthFraction = health / maxHealth;
        if (fillBackHealthBar > healthFraction)
        {
            frontHealthBar.fillAmount = healthFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBackHealthBar, healthFraction, percentComplete);
        }
        if (fillFrontHealthBar < healthFraction)
        {
            backHealthBar.fillAmount = healthFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFrontHealthBar, backHealthBar.fillAmount, percentComplete);
        }

    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
        durationTimerH = 0;
        overlayH.color = new Color(overlayH.color.r, overlayH.color.g, overlayH.color.b, 1);

    }

    public void HealthOverlayCheck()
    {
        if (overlayH.color.a > 0) //heal overlay
        {

            if (health < 80)
            {
                durationTimerH += Time.deltaTime;
                if (durationTimerH > durationH)
                {
                    float tempAlphaH = overlayH.color.a;
                    tempAlphaH -= Time.deltaTime * fadeSpeedH;
                    overlayH.color = new Color(overlayH.color.r, overlayH.color.g, overlayH.color.b, tempAlphaH);
                }
            }
            else
            {
                overlayH.color = new Color(overlayH.color.r, overlayH.color.g, overlayH.color.b, 0);
                durationTimerH = 0;
            }

        }
    }

    public void DamageOverlayCheck()
    {
        if (overlay.color.a > 0) //damage overlay
        {
            if (health < 30)
                return;
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }
}
