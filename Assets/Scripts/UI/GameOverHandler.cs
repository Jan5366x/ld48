using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public string nextLevel;
    public static bool victoryCondition;

    public void OnDeath()
    {
        LoadScene("DeathScene");
    }

    public void OnVictory()
    {
        if (victoryCondition)
        {
            victoryCondition = false;
            LoadScene(nextLevel);
        }
    }

    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}