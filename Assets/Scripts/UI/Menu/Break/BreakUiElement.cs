using UI.Base;
using UnityEngine;

public class BreakUiElement : BaseUiElement
{
    public GameObject Banner;
    public GameObject MenuButtons;

    #region Others and from other displays

    public GameObject ControlAndSound;

    #endregion

    private void Start()
    {
        this.ControlAndSound.SetActive(true);
    }

    #region ueberschriebene methode aus BaseUiElement
    
    /// <inheritdoc cref="BaseUiElement"/>
    public override void ScaleElements(float aspect)
    {
        this.Banner.ScaleByAspect(aspect);
        this.MenuButtons.ScaleByAspect(aspect);
    }

    /// <inheritdoc cref="BaseUiElement"/>
    public override void Show()
    {
        this.gameObject.SetActive(true);
        this.ControlAndSound.SetActive(true);

        GameState.GameIsRun = true;
    }

    /// <inheritdoc cref="BaseUiElement"/>
    public override void Hide()
    {
        this.gameObject.SetActive(false);
    }
    
    #endregion
}