using UI.Base;
using UnityEngine;

public class ControlAndSoundUiElement : BaseUiElement
{
    public GameObject soundOption;
    public GameObject controlDescriptions;

    #region ueberschriebene methode aus BaseUiElement
    
    /// <inheritdoc cref="BaseUiElement"/>
    public override void ScaleElements(float aspect)
    {
        this.soundOption.ScaleByAspect(aspect);
        this.controlDescriptions.ScaleByAspect(aspect);
    }

    /// <inheritdoc cref="BaseUiElement"/>
    public override void Show()
    {
        this.gameObject.SetActive(true);
    }

    /// <inheritdoc cref="BaseUiElement"/>
    public override void Hide()
    {
        this.gameObject.SetActive(false);
    }
    
    #endregion
}