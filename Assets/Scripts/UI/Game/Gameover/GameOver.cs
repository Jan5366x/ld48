using UI.Base;
using UnityEngine;

public class GameOver : BaseUiElement
{
    public void ButtonReturnToMenu()
    {
        Debug.Log("Script Game over. Set game status to false");
        // kann ggf. weg, wenn der aktive Spiel abgelesen werden kann
        GameState.GameIsRun = false;
    }
    
    #region ueberschriebene methode aus BaseUiElement

    /// <inheritdoc cref="BaseUiElement"/>
    public override void ScaleElements(float aspect)
    {
        this.gameObject.ScaleByAspect(aspect);
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