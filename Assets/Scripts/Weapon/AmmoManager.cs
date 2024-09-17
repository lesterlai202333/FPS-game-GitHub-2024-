using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{

    //UI
    public TextMeshProUGUI ammoDisplay;
    public static AmmoManager Instance { get; set; }

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

    // Update is called once per frame
    void Update()
    {

    }
}
