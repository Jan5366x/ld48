using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject target;
    public float speed = 10;
    public float damage = 10;

    private void Update()
    {
        if (target)
        {
            Vector3 dir = target.transform.position - transform.position;
            dir.Normalize();
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger && other.gameObject.Equals(target))
        {
            target.GetComponent<Enemy>().entity.TakeDamage(target.transform, damage);
            Destroy(gameObject);
        }
    }
}