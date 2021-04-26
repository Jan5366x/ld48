using UI.Base;
using UnityEngine;

public class MainUiElement : BaseUiElement
{
    #region windows

    public void ButtonCredit()
    {
        this.credits.SetActive(true);
        this.menuButtons.SetActive(false);
    }

    public void ButtonHighScore()
    {
        Debug.Log("Show Highscore");

        this.highscore.SetActive(true);
        this.menuButtons.SetActive(false);
        this.returnToGame.SetActive(false);
    }
    
    #endregion
    
    public void ButtonShowMainMenu()
    {
        Debug.Log("Open Main Menu from break display");
        this.gameObject.SetActive(true);
        this.menuButtons.SetActive(true);

        this.returnToGame.SetActive(GameState.GameIsRun);
    }
    
    #region Main Menu

    public GameObject menuButtons;
    public GameObject returnToGame;

    #endregion

    #region windows

    public GameObject highscore;
    public GameObject credits;

    #endregion

    
    
    public void ButtonBackToMainMenu()
    {
        this.highscore.SetActive(false);
        this.credits.SetActive(false);
    
        this.menuButtons.SetActive(true);
        this.returnToGame.SetActive(GameState.GameIsRun);
    }

    #region ueberschriebene methode aus BaseUiElement

    /// <inheritdoc cref="BaseUiElement"/>
    public override void ScaleElements(float aspect)
    {
        this.menuButtons.ScaleByAspect(aspect);
        this.highscore.ScaleByAspect(aspect);
        this.credits.ScaleByAspect(aspect);
        this.returnToGame.ScaleByAspect(aspect);
    }

    /// <inheritdoc cref="BaseUiElement"/>
    public override void Show()
    {
        this.gameObject.SetActive(true);
        this.highscore.SetActive(false);
        this.credits.SetActive(false);
        
        this.returnToGame.SetActive(GameState.GameIsRun);
    }

    /// <inheritdoc cref="BaseUiElement"/>
    public override void Hide()
    {
        this.gameObject.SetActive(false);
    }

    #endregion
}