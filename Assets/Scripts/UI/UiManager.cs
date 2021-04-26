﻿using System.Collections.Generic;
using UI.Base;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public Camera cam;

    public MainUiElement mainUiElement;
    public BreakUiElement breakUiElement;
    public ControlAndSound controlAndSound;
    public ScoreResult scoreResult;

    public HeadUpDisplay hud;
    public GameOver gameOver;

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
    }

    private void Update()
    {
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
        this._menus.Show<ControlAndSound>();
    }

    public void ButtonPlay()
    {
        this.AllHide();
        this._menus.Show<HeadUpDisplay>();

        // TODO Start game
    }

    public void ButtonContinuos()
    {
        this.AllHide();
        this._menus.Show<HeadUpDisplay>();

        // TODO resum game
    }

    public void ButtonScoreResult()
    {
        this.AllHide();
        this._menus.Show<ScoreResult>();
    }
    
    public void ButtonGameOverShow()
    {
        this.AllHide();
        this._menus.Show<GameOver>();
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
        Application.Quit();
    }

    #endregion

    
}