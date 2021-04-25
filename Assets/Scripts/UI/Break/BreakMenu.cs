using UI.MainMenu;
using UnityEngine;

public class BreakMenu : MonoBehaviour
{
    public Camera Camera;
    
    public GameObject GameHUD;
    
    public GameObject Banner;
    public GameObject MenuButtons;

    #region Others and from other displays

    public GameObject ControlAndSound;

    #endregion
    
    private void Start()
    {
        this.ScaleMenuItems();
        
        this.ControlAndSound.SetActive(true);
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
        
        // TODO: Test, sollte zugewiesen vom aktuellen GameState
        GameState.GameIsRun = true;
    }

    public void ButtonShowBreakMenu()
    {
        this.gameObject.SetActive(true);
        this.ControlAndSound.SetActive(true);
    }

    public void ButtonCloseTheGame()
    {
        Debug.Log("Close the game");
        Application.Quit();
    }
}