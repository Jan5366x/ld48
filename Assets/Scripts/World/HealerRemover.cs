using UnityEngine;

public class HealerRemover : MonoBehaviour
{
    private void Start()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = -Mathf.RoundToInt(transform.position.y);

        WorldController.DeleteHealer(x, y);
    }
}