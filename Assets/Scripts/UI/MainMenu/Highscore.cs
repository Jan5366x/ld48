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
        this.SetValues(this._test[0], this.PlayerHighscoreItem1);
        this.SetValues(this._test[1], this.PlayerHighscoreItem2);
        this.SetValues(this._test[2], this.PlayerHighscoreItem3);
        this.SetValues(this._test[3], this.PlayerHighscoreItem4);
        this.SetValues(this._test[4], this.PlayerHighscoreItem5);
    }

    private void SetValues((string, long) item, GameObject gameObject)
    {
        var hsi = gameObject.GetComponent<HighscoreItem>();
        hsi.SetValues(item.Item1, item.Item2);
    }
}