using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelUiManager : MonoBehaviour
{
    public GameObject pause;
    public bool isPaused;
    void Start()
    {
        pause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

    }
    public void ResumeGame()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void PauseGame()
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
