
using TMPro;
using UI.Base;
using UnityEngine;

public class MainMenu : BaseMenu
{
    #region windows

    public void ButtonCredit()
    {
        this.Credits.SetActive(true);
        this.MenuButtons.SetActive(false);
    }

    public void ButtonHighScore()
    {
        Debug.Log("Show Highscore");

        this.Highscore.SetActive(true);
        this.MenuButtons.SetActive(false);
        this.ReturnToGame.SetActive(false);
    }
    
    #endregion
    
    public void ButtonShowMainMenu()
    {
        Debug.Log("Open Main Menu from break display");
        this.gameObject.SetActive(true);
        this.MenuButtons.SetActive(true);

        this.ReturnToGame.SetActive(GameState.GameIsRun);
    }
    
    #region Main Menu

    // public GameObject BordTitle;
    // public GameObject BordSubTitle;
    public GameObject MenuButtons;
    public GameObject ReturnToGame;

    #endregion

    #region windows

    public GameObject Highscore;
    public GameObject Credits;

    #endregion

    public override void ScaleElements(float aspect)
    {
        this.MenuButtons.ScaleByAspect(aspect);
        this.Highscore.ScaleByAspect(aspect);
        this.Credits.ScaleByAspect(aspect);
        this.ReturnToGame.ScaleByAspect(aspect);

        // if (this.camera.pixelWidth <= 800)
        // {
        //     var hs = this.BordTitle.GetComponent<TMP_Text>();
        //     hs.fontSize = 80f * aspect / MenuHelper.DefaultRatio;
        //
        //     var hsSubTitle = this.BordSubTitle.GetComponent<TMP_Text>();
        //     hsSubTitle.fontSize = 50f * aspect / MenuHelper.DefaultRatio;
        // }
    }
    
    public void ButtonBackToMainMenu()
    {
        this.Highscore.SetActive(false);
        this.Credits.SetActive(false);
    
        this.MenuButtons.SetActive(true);
        this.ReturnToGame.SetActive(GameState.GameIsRun);
    }

    public override void Show()
    {
        this.gameObject.SetActive(true);
        this.Highscore.SetActive(false);
        this.Credits.SetActive(false);
        
        Debug.Log($"GameState GameIsRun: {GameState.GameIsRun}");
        this.ReturnToGame.SetActive(GameState.GameIsRun);
    }

    public override void Hide()
    {
        this.gameObject.SetActive(false);
    }
}