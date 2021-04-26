using System;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    public GameObject PlayerHighscoreItem1;
    public GameObject PlayerHighscoreItem2;
    public GameObject PlayerHighscoreItem3;
    public GameObject PlayerHighscoreItem4;
    public GameObject PlayerHighscoreItem5;

    private readonly (string, long)[] _test =
    {
        ("Player 1", 11111111111),
        ("Player 2", 22222222222),
        ("Player 3", 33333333333),
        ("Player 4", 44444444444),
        ("Player 5", 44444444444)
    };

    private void Start()
    {
        Debug.Log("Load score results from Start");
        var data = SaveManager.Load();

        SetValues(data.Results[0], this.PlayerHighscoreItem1);
        SetValues(data.Results[1], this.PlayerHighscoreItem2);
        SetValues(data.Results[2], this.PlayerHighscoreItem3);
        SetValues(data.Results[3], this.PlayerHighscoreItem4);
        SetValues(data.Results[4], this.PlayerHighscoreItem5);
    }

    private void OnEnable()
    {
        
        Debug.Log("Load score results from OnEnable");
        var data = SaveManager.Load();

        SetValues(data.Results[0], this.PlayerHighscoreItem1);
        SetValues(data.Results[1], this.PlayerHighscoreItem2);
        SetValues(data.Results[2], this.PlayerHighscoreItem3);
        SetValues(data.Results[3], this.PlayerHighscoreItem4);
        SetValues(data.Results[4], this.PlayerHighscoreItem5);
    }

    private static void SetValues(PlayerHighScoreItem item, GameObject gameObject)
    {
        var hsi = gameObject.GetComponent<HighscoreItem>();
        hsi.SetValues(item.PlayerName, item.ScoreResult);
    }
}