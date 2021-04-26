using TMPro;
using UI.Base;
using UnityEngine;

public class ScoreResultUiElement : BaseUiElement
{
    public GameObject banner;
    public GameObject scoreContent;

    public TMP_Text resultText;
    public TMP_InputField inputPlayername;

    public void SavePlayernameAndResult()
    {
        // TODO: Implement save
        var result = this.inputPlayername.text;
        
        Debug.Log($"Playername: {result}, result value: {this.resultText.text}");
    }
    
    #region ueberschriebene methode aus BaseUiElement
    
    /// <inheritdoc cref="BaseUiElement"/>
    public override void ScaleElements(float aspect)
    {
        this.banner.ScaleByAspect(aspect);
        this.scoreContent.ScaleByAspect(aspect);
    }

    /// <inheritdoc cref="BaseUiElement"/>
    public override void Show()
    {
        this.gameObject.SetActive(true);

        GameState.GameIsRun = false;
    }

    /// <inheritdoc cref="BaseUiElement"/>
    public override void Hide()
    {
        this.gameObject.SetActive(false);
    }
    
    #endregion
}