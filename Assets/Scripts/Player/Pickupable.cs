using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public int money;
    public int healing;
    public Transform onPickupPrefab;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponentInChildren<Player>();
        if (player)
        {
            Player.CollectMoney(other.transform, money);
            Player.entity.Heal(healing);
            Instantiate(onPickupPrefab, other.transform);
            Destroy(gameObject);
        }
    }
}