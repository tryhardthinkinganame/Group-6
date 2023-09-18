using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;

public class PauseScreenBehaviour : MainMenuBehaviour
{

    public static bool paused;

    [Tooltip("Reference to the pause menu object to turn on/off")]
    public GameObject pauseMenu;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     
    }

    public void SetPauseMenu(bool isPaused)
    {
        paused = isPaused;

        Time.timeScale = (paused) ? 0 : 1;
        pauseMenu.SetActive(paused);
    }
    void Start()
    {
        SetPauseMenu(false);
    }

    #region Share Score via Twitter
    /// <summary>
    /// Web address in order to create a tweet
    /// </summary>
    private const string tweetTextAddress = "http://twitter.com/intent/tweet?text=";

    /// <summary>
    /// Where we want players to visit
    /// </summary>
    private string appStoreLink = "http://johnpdoran.com/";

    [Tooltip("Reference to the player for the score")]
    public PlayerBehaviour player;

    /// <summary>
    /// Will open Twitter with a prebuilt tweet. When called on iOS or
    /// Android will open up Twitter app if installed
    /// </summary>
    public void TweetScore()
    {
        int highScoreInt = Mathf.FloorToInt(player.highScore);

        // Create contents of the tweet
        string tweet = "I got " + string.Format("{0:0}", player.Score)+ " points in Endless Roller! Can you do better? My High Score is "+highScoreInt+"!";
        
        // Create the entire message
        string message = tweet + "\n" + appStoreLink;

        //Ensures string is URL friendly
        string url = UnityEngine.Networking.UnityWebRequest.EscapeURL(message);
       
        // Open the URL to create the tweet
        Application.OpenURL(tweetTextAddress + url);

    }
    #endregion
}

