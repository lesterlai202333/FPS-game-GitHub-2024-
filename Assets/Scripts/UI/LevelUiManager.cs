using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class LevelUiManager : MonoBehaviour
{
    public GameObject pause;
    public bool isPaused;
    public AudioSource SoundEffect;
    public AudioSource PlayGameEffect;


    void Awake()
    {
        pause.SetActive(false);

        isPaused = false;


    }

    // Update is called once per frame
    public void OnTogglePause(InputAction.CallbackContext ctxt)
    {
        if (ctxt.performed)
        {
            isPaused = !isPaused;
            SoundEffect.Play();
            {
                if (isPaused)
                {
                    Pause();

                }
                else
                {
                    Resume();
                }
            }
        }
    }

    public void Resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        SoundEffect.Play();
    }
    public void Pause()
    {
        pause.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ExitLevel()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene("StartPage 1");
        SoundEffect.Play();
    }
    public void SetFullscreen(bool _fullscreen)
    {
        bool fullScreen = Screen.fullScreen;
        Screen.fullScreen = _fullscreen;

    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("Scene 1");
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
