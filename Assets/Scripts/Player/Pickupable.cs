using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public int money;
    public int healing;
    public Transform onPickupPrefab;

    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponentInChildren<Player>();
        if (!player) return;
        if (collected) return;
        collected = true;
        Player.CollectMoney(other.transform, money);
        Player.entity.Heal(other.transform, healing);
        if (onPickupPrefab)
        {
            Instantiate(onPickupPrefab, other.transform);
        }

        Destroy(gameObject);
    }
}