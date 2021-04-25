using TMPro;
using UnityEngine;

public class PlayTimeCounter : MonoBehaviour
{
    public TMP_Text text;

    private void OnGUI()
    {
        text.text = "" + (Time.time - Player.timeLoad);
    }
}