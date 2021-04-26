using UI.Base;
using UnityEngine;

public class ControlAndSound : BaseMenu
{
    public GameObject soundOption;
    public GameObject controlDescriptions;

    public override void ScaleElements(float aspect)
    {
        this.soundOption.ScaleByAspect(aspect);
        this.controlDescriptions.ScaleByAspect(aspect);
    }

    public override void Show()
    {
        this.gameObject.SetActive(true);
    }

    public override void Hide()
    {
        this.gameObject.SetActive(false);
    }
}