using System;
using UI.MainMenu;
using UnityEngine;

public class BreakMenu : MonoBehaviour
{
    public Camera Camera;
    
    public GameObject GameHUD;
    
    public GameObject Banner;
    public GameObject MenuButtons;

    private void Start()
    {
        ScaleMenuItems();
    }

    private void Update()
    {
        
        
        // TODO: nur beim testen nötig
        ScaleMenuItems();
    }

    private void ScaleMenuItems()
    {
        var aspect = this.Camera.aspect;
        this.Banner.ScaleByAspect(aspect);
        this.MenuButtons.ScaleByAspect(aspect);
    }

    public void ButtonContinue()
    {
        this.gameObject.SetActive(false);
        this.GameHUD.SetActive(true);
    }

    public void ButtonToMainMenu()
    {
        this.gameObject.SetActive(false);
    }

    public void ButtonCloseTheGame()
    {
        Application.Quit();
    }
}