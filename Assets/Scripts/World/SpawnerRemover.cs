using UnityEngine;

public class SpawnerRemover : MonoBehaviour
{
    private void Start()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = -Mathf.RoundToInt(transform.position.y);

        WorldController.DeleteSpawner(x, y);
    }
}