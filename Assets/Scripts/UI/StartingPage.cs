using UnityEngine;
using System.Collections.Generic;
public class StartingPage : MonoBehaviour
{
    public GameObject settingsPage;
    public AudioSource ButtonSoundEffect;
    public AudioSource PlayGameEffect; //declaring audiosources and Gameobjects to be assigned in the unity editor    
    void Start()
    {
        settingsPage.SetActive(false); //the settings page is set inactive when the scene starts
    }
    public void SetFullscreen(bool _fullscreen)//The SetFullscreen function toggles the fullscreen mode of the application based on the value of the _fullscreen parameter. If _fullscreen is true, it switches to fullscreen mode; if false, it switches to windowed mode.
    {
        bool fullScreen = Screen.fullScreen; //This line retrieves the current fullscreen state of the application (i.e., whether the game is currently in fullscreen mode) and stores it in a local variable called fullScreen.

        Screen.fullScreen = _fullscreen; //This line sets the fullscreen state of the application to the value of _fullscreen.
    }
    public void TabAndButtonSoundEffect()
    {
        ButtonSoundEffect.Play(); //play the buttonsoundeffect when this function is called 
    }
    public void PlayGameSound()
    {
        PlayGameEffect.Play(); //plays the playGamesoundeffect when this function is called
    }

}
