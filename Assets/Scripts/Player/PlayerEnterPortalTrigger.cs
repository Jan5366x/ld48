using UnityEngine;

public class PlayerEnterPortalTrigger : MonoBehaviour
{
    private void Update()
    {
        GetComponent<SpriteRenderer>().color = GameOverHandler.victoryCondition ? Color.green : Color.red;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            GameObject.FindWithTag("MainCamera").GetComponent<GameOverHandler>().OnVictory();
        }
    }
}