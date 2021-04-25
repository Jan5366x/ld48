using TMPro;
using UI.Base;
using UnityEngine;

public class ScoreResult : BaseMenu
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
    
    
    public override void ScaleElements(float aspect)
    {
        this.banner.ScaleByAspect(aspect);
        this.scoreContent.ScaleByAspect(aspect);
    }

    public override void Show()
    {
        this.gameObject.SetActive(true);

        GameState.GameIsRun = false;
    }

    public override void Hide()
    {
        this.gameObject.SetActive(false);
    }
}