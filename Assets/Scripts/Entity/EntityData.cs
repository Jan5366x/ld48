using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class EntityData
{
    public float health;
    public float maxHealth;
    public Transform onDeathPrefab;
    public Transform onDamagePrefab;
    public Rect splatterArea;

    public bool wasDead = false;

    public void Heal(Transform transform, float amount)
    {
        if (isDead())
        {
            return;
        }

        if (amount > 0)
        {
            RandomizedSound.Play(transform, RandomizedSound.HEAL);
        }

        health = Mathf.Max(maxHealth, health + amount);
    }

    public void TakeDamage(Transform transform, float amount)
    {
        health -= amount;

        Transform newTransform = Transform.Instantiate(onDamagePrefab, transform);
        newTransform.Translate(Random.Range(splatterArea.xMin, splatterArea.xMax),
            Random.Range(splatterArea.yMin, splatterArea.yMax), 0);
        Animation animator = newTransform.GetComponent<Animation>();
        if (animator)
        {
            animator.wrapMode = WrapMode.Once;
        }

        RandomizedSound.Play(transform, RandomizedSound.HURT);

        if (health < 0)
        {
            Die(transform);
        }
    }

    private void Die(Transform transform)
    {
        wasDead = true;
        if (onDeathPrefab)
        {
            Transform.Instantiate(onDeathPrefab, transform.position, transform.rotation);
        }

        RandomizedSound.Play(transform, RandomizedSound.DIE);

        // GameOverToggler.OnDeath();

        Transform.Destroy(transform.gameObject);
    }

    public bool isDead()
    {
        return wasDead || health < 0;
    }
}