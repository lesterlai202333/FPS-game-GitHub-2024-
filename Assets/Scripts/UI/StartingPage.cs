using UnityEngine;
using System.Collections.Generic;
public class StartingPage : MonoBehaviour
{
    public GameObject settingsPage;
    public AudioSource SoundEffect;
    public AudioSource PlayGameEffect;
    void Start()
    {
        settingsPage.SetActive(false);
    }
    public void SetFullscreen(bool _fullscreen)
    {
        bool fullScreen = Screen.fullScreen;
        Screen.fullScreen = _fullscreen;

    }
    public void TabAndButtonSoundEffect()
    {
        SoundEffect.Play();
    }
    public void PlayGameSound()
    {
        PlayGameEffect.Play();
    }

}
