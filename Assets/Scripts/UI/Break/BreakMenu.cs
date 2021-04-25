using UI.Base;
using UnityEngine;

public class BreakMenu : BaseMenu
{
    public GameObject GameHUD;

    public GameObject Banner;
    public GameObject MenuButtons;

    #region Others and from other displays

    public GameObject ControlAndSound;

    #endregion

    private void Start()
    {
        this.ControlAndSound.SetActive(true);
    }

    public override void ScaleElements(float aspect)
    {
        this.Banner.ScaleByAspect(aspect);
        this.MenuButtons.ScaleByAspect(aspect);
    }

    public override void Show()
    {
        this.gameObject.SetActive(true);
        this.ControlAndSound.SetActive(true);

        GameState.GameIsRun = true;
    }

    public override void Hide()
    {
        this.gameObject.SetActive(false);
    }
}