using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class LevelUiManager : MonoBehaviour
{
    public GameObject pause;
    public bool isPaused;
    void Start()
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
    }
}
