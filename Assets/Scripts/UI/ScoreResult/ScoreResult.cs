using System;
using UI.MainMenu;
using UnityEngine;

public class ScoreResult : MonoBehaviour
{
    public Camera Camera;

    public GameObject Banner;
    public GameObject ScoreContent;

    public GameObject HUD;
    private void Start()
    {
        this.ScaleMenuItems();
    }

    private void Update()
    {
        this.ScaleMenuItems();
    }

    public void ButtonClose()
    {
        this.gameObject.SetActive(false);
    }

    private void ScaleMenuItems()
    {
        var aspect = this.Camera.aspect;
        this.Banner.ScaleByAspect(aspect);
        this.ScoreContent.ScaleByAspect(aspect);
    }

    public void ActionHideHUD()
    {
        this.HUD.SetActive(false);
    }
}