using System;
using TMPro;
using UnityEngine;

public class PlayTimeCounter : MonoBehaviour
{
    public TMP_Text text;

    private void OnGUI()
    {
        TimeSpan t = TimeSpan.FromSeconds(GameOverHandler.gameEndTime - Player.timeLoad);
        text.text = t.ToString(@"hh\:mm\:ss");
    }
}