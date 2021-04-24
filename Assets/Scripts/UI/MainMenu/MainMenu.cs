using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Highscore;

    private void Start()
    {
        this.Highscore.SetActive(false);
    }

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
        Debug.Log("Show Highscore");
        
        this.Highscore.SetActive(true);
    }
}
