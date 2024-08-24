using UnityEngine;
using System.Collections.Generic;
public class StartingPage : MonoBehaviour
{
    public GameObject settingsPage;
    void Start()
    {
        settingsPage.SetActive(false);
    }
    public void SetFullscreen(bool _fullscreen)
    {
        Screen.fullScreen = _fullscreen;

    }

}
