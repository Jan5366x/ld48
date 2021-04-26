using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public string nextLevel;
    public static bool gameOver;
    public static bool victoryCondition;
    public static float gameEndTime;
    public bool isFinal = false;

    public void OnDeath()
    {
        gameOver = true;
        gameEndTime = Time.time;
        LoadScene("DieScene");
    }

    public void OnVictory()
    {
        if (victoryCondition)
        {
            if (isFinal)
            {
                OnFinalVictory();
            }
            else
            {
                victoryCondition = false;
                LoadScene(nextLevel);
            }
        }
    }

    public void OnFinalVictory()
    {
        gameOver = true;
        gameEndTime = Time.time;
        LoadScene("WinScene");
    }

    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    private void Update()
    {
        if (!gameOver)
        {
            gameEndTime = Time.time;
        }
    }
}