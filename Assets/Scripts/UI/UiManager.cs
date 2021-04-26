using System.Collections.Generic;
using UI.Base;
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

    public void Start()
    {
        this._menus.Add(this.mainUiElement);
        this._menus.Add(this.controlAndSound);
        this._menus.Add(this.breakUiElement);
        this._menus.Add(this.scoreResult);
        this._menus.Add(this.hud);
        this._menus.Add(this.gameOver);
        this.ButtonMainMenuShow();

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

        if (!GameState.GameIsRun)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var r = SceneManager.GetSceneAt(i);
                Debug.Log($"Scene: {r.name}");

                if (r.name.Equals(_gameScene))
                {
                    SceneManager.UnloadSceneAsync(_gameScene);
                }
            }
        }
    }

    public void ButtonPlay()
    {
        this.AllHide();
        this._menus.Show<HeadUpDisplayUiElement>();

        // TODO Start game

        SceneManager.LoadScene(_gameScene, LoadSceneMode.Additive);
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
        SceneManager.UnloadSceneAsync(_gameScene);

        Debug.Log("Close the game");
        Application.Quit();
    }

    #endregion
}