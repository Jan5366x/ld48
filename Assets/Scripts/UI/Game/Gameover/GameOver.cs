using UI.Base;

public class GameOver : BaseMenu
{
    public void ButtonReturnToMenu()
    {
        // kann ggf. weg, wenn der aktive Spiel abgelesen werden kann
        GameState.GameIsRun = false;
    }

    public override void ScaleElements(float aspect)
    {
        this.gameObject.ScaleByAspect(aspect);
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