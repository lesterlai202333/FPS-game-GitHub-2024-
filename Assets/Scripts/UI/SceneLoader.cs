using UnityEngine;
using UnityEngine.SceneManagement; //needed for scene transition
public class SceneLoader : MonoBehaviour
{
    public void LevelOne()
    {
        SceneManager.LoadScene("Scene 1"); //transtions to the Scene 1 when this function is called
    }


    public void QuitGame()
    {
        Application.Quit(); //quits the application when this function is called
    }
}
