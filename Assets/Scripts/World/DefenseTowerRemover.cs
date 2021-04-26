using UnityEngine;

public class DefenseTowerRemover : MonoBehaviour
{
    private void Start()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = -Mathf.RoundToInt(transform.position.y);

        WorldController.DeleteDefenseTower(x, y);
    }
}