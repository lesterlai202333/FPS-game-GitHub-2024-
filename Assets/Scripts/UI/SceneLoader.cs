using UnityEngine;
using UnityEngine.SceneManagement; //needed for scene transition
public class SceneLoader : MonoBehaviour
{
    public void LevelOne()//public --> can be accessed from outside of this script, void means no value returned
    {
        SceneManager.LoadScene("Scene 1"); //transtions to the Scene 1 when this function is called
    }


    public void QuitGame()//public --> can be accessed from outside of this script, void means non value returned
    {
        Application.Quit(); //quits the application when this function is called
    }
}
