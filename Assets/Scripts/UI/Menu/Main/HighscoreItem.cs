using TMPro;
using UnityEngine;

public class HighscoreItem : MonoBehaviour
{
    public GameObject Playername;
    public GameObject Score;


    public void SetValues(string playerName, long score)
    {
        var pn = this.Playername.gameObject.GetComponent<TMP_Text>();
        if (pn == null)
        {
            return;
        }

        pn.text = playerName;

        var s = this.Score.GetComponent<TMP_Text>();
        if (s == null)
        {
            return;
        }

        s.text = $"{score}";
    }
}