using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void LevelOne()
    {
        SceneManager.LoadScene("Scene 1");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
