using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Camera Camera;

    public GameObject BordTitle;
    
    public GameObject Highscore;

    
    public GameObject MenuButtons;

    public GameObject ControlDescription;

    private void Update()
    {
   
        // TODO: kommt in start und dient zum debuggen.
        this.ScaleMenuItems();
        
    }

    private void ScaleMenuItems()
    {
        var aspect = this.Camera.aspect;
        this.MenuButtons.transform.localScale = new Vector3(1, 1, 1) * aspect;
        this.ControlDescription.transform.localScale =  new Vector3(1, 1, 1) * aspect;
    }

    private void Start()
    {
        this.Highscore.SetActive(false);
    }

    public void ButtonCredit()
    {
        
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
