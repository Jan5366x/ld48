using TMPro;
using UI.MainMenu;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Camera Camera;

    #region Others and from other displays

    public GameObject ControlAndSound;
    public GameObject Hud;

    #endregion

    private void Start()
    {
        this.ScaleMenuItems();
        this.Highscore.SetActive(false);
        this.Credits.SetActive(false);
        this.ReturnToGame.SetActive(false);

        this.ControlAndSound.SetActive(true);
    }

    private void Update()
    {
        // TODO: nur beim testen nötig
        this.ScaleMenuItems();
    }

    private void ScaleMenuItems()
    {
        var aspect = this.Camera.aspect;
        Debug.Log($"ASpect: {aspect}");
        this.MenuButtons.ScaleByAspect(aspect);
        this.Highscore.ScaleByAspect(aspect);
        this.Credits.ScaleByAspect(aspect);
        this.ReturnToGame.ScaleByAspect(aspect);

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
        this.MenuButtons.SetActive(false);
    }

    public void ButtonExit()
    {
        Debug.Log("Close the game");
        Application.Quit();
    }

    public void ButtonPlaying()
    {
        this.Hud.SetActive(true);
        this.gameObject.SetActive(false);
        this.ControlAndSound.SetActive(false);
        
        
        // TODO: Setup here the main game content.
        //SceneManager.LoadScene("TYPE THE MAIN GAME CONTENT");
        //this.MenuButtons.SetActive(false);
        
    }

    public void ButtonHighscore()
    {
        Debug.Log("Show Highscore");

        this.Highscore.SetActive(true);
        this.MenuButtons.SetActive(false);
    }

    public void ButtonBackToMainMenu()
    {
        this.Highscore.SetActive(false);
        this.Credits.SetActive(false);

        this.MenuButtons.SetActive(true);
    }

    public void ButtonShowMainMenu()
    {
        Debug.Log("Open Main Menu from break display");
        this.gameObject.SetActive(true);
        this.MenuButtons.SetActive(true);

        this.ReturnToGame.SetActive(GameState.GameIsRun);
    }

    public void ButtonHideMainMenu()
    {
        this.gameObject.SetActive(false);
    }

    #region Main Menu

    public GameObject BordTitle;
    public GameObject BordSubTitle;
    public GameObject MenuButtons;
    public GameObject ReturnToGame;

    #endregion

    #region windows

    public GameObject Highscore;
    public GameObject Credits;

    #endregion
}