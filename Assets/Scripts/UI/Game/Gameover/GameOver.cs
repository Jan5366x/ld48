using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void ButtonReturnToMenu()
    {
        // kann ggf. weg, wenn der aktive Spiel abgelesen werden kann
        GameState.GameIsRun = false;
    }
}
