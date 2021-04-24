using TMPro;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Camera Camera;

    public GameObject BordTitle;
    public GameObject BordSubTitle;
    
    public GameObject Highscore;

    public GameObject MenuButtons;

    public GameObject ControlDescription;
    
    public GameObject Credits;

    private void Start()
    {
        this.Highscore.SetActive(false);
        this.Credits.SetActive(false);
    }
    
    private void Update()
    {
   
        // TODO: kommt in start und dient zum debuggen.
        this.ScaleMenuItems();
        
        
        Debug.Log($"a:{this.Camera.aspect} w:{this.Camera.pixelWidth} d:{this.Camera.depth}");
    }

    private void ScaleMenuItems()
    {
        var aspect = this.Camera.aspect;
        Debug.Log($"ASpect: {aspect}");
        this.MenuButtons.ScaleByAspect(aspect);
        this.ControlDescription.ScaleByAspect(aspect);
        this.Highscore.ScaleByAspect(aspect);
        this.Credits.ScaleByAspect(aspect);

        if (this.Camera.pixelWidth <= 800)
        {
            var hs = this.BordTitle.GetComponent<TMP_Text>();
            hs.fontSize = 80f * aspect / ResolutionHelper.DefaultRatio;

            var hsSubTitle = this.BordSubTitle.GetComponent<TMP_Text>();
            hsSubTitle.fontSize = 50f * aspect / ResolutionHelper.DefaultRatio;
        }
    }

    public void ButtonCredit()
    {
        this.Credits.SetActive(true);
        //this.MenuButtons.SetActive(false);
    }

    public void ButtonExit()
    {
        Application.Quit();
    }

    public void ButtonPlaying()
    {
        // TODO: Setup here the main game content.
        //SceneManager.LoadScene("TYPE THE MAIN GAME CONTENT");
        //this.MenuButtons.SetActive(false);
    }

    public void ButtonHighscore()
    {
        Debug.Log("Show Highscore");
        
        this.Highscore.SetActive(true);
        //this.MenuButtons.SetActive(false);
    }
    
    public void ButtonBackToMainMenu()
    {
        this.Highscore.SetActive(false);
        this.Credits.SetActive(false);
        
        // this.MenuButtons.SetActive(true);
    }
}
