using System.Collections.Generic;
using UI.Base;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    private const string _gameScene = "Codexzier_Workspace_Test";

    public Camera cam;

    public MainUiElement mainUiElement;
    public BreakUiElement breakUiElement;
    public ControlAndSoundUiElement controlAndSound;
    public ScoreResultUiElement scoreResult;

    public HeadUpDisplayUiElement hud;
    public GameOverUiElement gameOver;

    private readonly List<BaseUiElement> _menus = new List<BaseUiElement>();

    private void AddMenuEntry(BaseUiElement entry)
    {
        if (entry)
        {
            this._menus.Add(entry);
        }
    }

    public void Start()
    {
        AddMenuEntry(mainUiElement);
        AddMenuEntry(controlAndSound);
        AddMenuEntry(breakUiElement);
        AddMenuEntry(scoreResult);
        AddMenuEntry(hud);
        AddMenuEntry(gameOver);
        ButtonMainMenuShow();

        this.ScaleAllElements();
    }

    private void Update()
    {
        // TODO teuere Methode, kann am ende entfernt werden.
        this.ScaleAllElements();
    }

    public void ScaleAllElements()
    {
        var aspect = this.cam.aspect;
        foreach (var menu in this._menus)
        {
            menu.ScaleElements(aspect);
        }
    }

    #region Button methods

    public void ButtonBreakMenuShow()
    {
        this.AllHide();
        this._menus.Show<BreakUiElement>();
    }

    public void ButtonMainMenuShow()
    {
        this.AllHide();
        this._menus.Show<MainUiElement>();
        this._menus.Show<ControlAndSoundUiElement>();
    }

    public void ButtonPlay()
    {
        this.AllHide();
        this._menus.Show<HeadUpDisplayUiElement>();

        Player.ResetPlayerData();
        SceneManager.LoadScene("MainGame");
    }

    public void ButtonContinuos()
    {
        this.AllHide();
        this._menus.Show<HeadUpDisplayUiElement>();

        // TODO resum game
    }

    public void ButtonScoreResult()
    {
        this.AllHide();
        this._menus.Show<ScoreResultUiElement>();
    }

    public void ButtonGameOverShow()
    {
        this.AllHide();
        this._menus.Show<GameOverUiElement>();
    }

    private void AllHide()
    {
        foreach (var baseMenu in this._menus)
        {
            baseMenu.Hide();
        }
    }

    public void ButtonExit()
    {
        Debug.Log("Close the game");
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    #endregion
}