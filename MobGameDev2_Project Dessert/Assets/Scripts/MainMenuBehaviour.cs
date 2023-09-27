using UnityEngine;
using UnityEngine.SceneManagement; 
public class MainMenuBehaviour : MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        PlayerPrefs.DeleteKey("HighScore"); 

        SceneManager.LoadScene(levelName);
    }
}