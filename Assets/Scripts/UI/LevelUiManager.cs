using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class LevelUiManager : MonoBehaviour
{
    public GameObject pause;//public --> can be accessed from outside of this script, gameobject is the type of variable, last word is the name of the variable
    public bool isPaused;//public --> can be accessed from outside of this script, variable type is a boolean（true or false, last word is the name of the variable.
    public AudioSource SoundEffect;//public --> can be accessed from outside of this script, variable type is an audiosource, last word is name of the variable declared in the script.
    public AudioSource PlayGameEffect;//public --> can be accessed from outside of this script, variable type is an audiosource, last word is name of the variable declared in the script.
    public GameObject pauseContent;
    public GameObject settingsPage;

    void Awake()
    {
        pause.SetActive(false); //Deactivates the pause menu

        isPaused = false; //the game isn't paused


    }

    void Update()
    {
        if (isPaused) //if the game is paused then the mouse cursor becomes unlocked so allows you to use the UI(click buttons)
        {

            //locking the cursor to the middle of the screen and making it invisible
            Cursor.lockState = CursorLockMode.Confined;


        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;

        }


    }
    public void OnTogglePause(InputAction.CallbackContext ctxt)//public --> can be accessed from outside of this script
    {
        if (ctxt.performed) //Related to Keybind, if the key corresponding to this function is 
        {
            isPaused = !isPaused; //acts as a switch, true to false, false to true, it's like multiplying a number by negative one
            SoundEffect.Play(); //play the sound effect
            {
                if (isPaused)
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    Pause(); //if the game is paused, the pause menu is activated, time is frozen.....everything in the pause（） function

                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Resume(); //resumes the game

                }
            }
        }
    }

    public void Resume()//public --> can be accessed from outside of this script
    {
        pause.SetActive(false);
        pauseContent.SetActive(false);
        settingsPage.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        SoundEffect.Play(); //pause menu UI deactivated so disappears from screen, time set back to normal, the game isn't paused, and a sound effect is played



    }
    public void Pause()//public --> can be accessed from outside of this script
    {
        pause.SetActive(true); //pause menu set active, time frozen, the game is paused
        pauseContent.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ExitLevel()//public --> can be accessed from outside of this script
    {

        Time.timeScale = 1f; //The time scale of the scene is set to normal, loads the starting scene, plays another sound effect
        SceneManager.LoadScene("StartPage 1");
        SoundEffect.Play();
    }
    public void SetFullscreen(bool _fullscreen)//public --> can be accessed from outside of this script, It accepts a parameter _fullscreen of type bool. The parameter determines whether the screen should be set to fullscreen (true) or windowed (false).
    {
        bool fullScreen = Screen.fullScreen; //This line retrieves the current fullscreen state of the application (i.e., whether the game is currently in fullscreen mode) and stores it in a local variable called fullScreen.
        Screen.fullScreen = _fullscreen; //This line sets the fullscreen state of the application to the value of _fullscreen.

    }
    public void RestartLevel()//public --> can be accessed from outside of this script
    {
        SceneManager.LoadScene("Scene 1"); //reloads this scene
    }
    public void RestartLachlan()//public --> can be accessed from outside of this script
    {
        SceneManager.LoadScene("Lachlan Scene"); //reloads this scene
    }

    public void TabAndButtonSoundEffect()//public --> can be accessed from outside of this script
    {
        SoundEffect.Play(); //soundeffect
    }
    public void PlayGameSound()//public --> can be accessed from outside of this script
    {
        PlayGameEffect.Play(); //soundeffect
    }
}
