using TMPro;
using UnityEngine;

public class PlayerMoneyCounter : MonoBehaviour
{
    public TMP_Text text;

    private void OnGUI()
    {
        text.text = "" + Player.numMoney;
    }
}