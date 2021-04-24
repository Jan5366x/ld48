using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ButtonExit()
    {
        Application.Quit();
    }

    public void ButtonPlaying()
    {
        // TODO: Setup here the main game content.
        SceneManager.LoadScene("TYPE THE MAIN GAME CONTENT");
    }

    public void ButtonHighscore()
    {
        SceneManager.LoadScene("Scenes/Highscore");
    }
}
