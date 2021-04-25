using UI.Base;
using UnityEngine;

public class ControlAndSound : BaseMenu
{
    public GameObject SoundOption;
    public GameObject ControlDescriptions;

    public override void ScaleElements(float aspect)
    {
        this.SoundOption.ScaleByAspect(aspect);
        this.ControlDescriptions.ScaleByAspect(aspect);
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