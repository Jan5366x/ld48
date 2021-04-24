using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    public float coolDown;
    public float coolDownCounter;
    // public List<Entity> entitiesInRange = new List<Entity>();
    public bool used;
    public Transform dropWeapon;

    // public abstract bool UseOn(Entity entity);
    public abstract Animator GetAnimator();

    void Update()
    {
        if (used)
        {
            Animator animator = GetAnimator();
            if (animator)
            {
                animator.speed = 1 / coolDown;
                animator.SetTrigger("OnHit");
            }

            coolDownCounter = coolDown;
            used = false;
        }

        coolDownCounter -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Entity otherEntity = other.GetComponent<Entity>();
        // if (otherEntity)
        // {
            // entitiesInRange.Add(otherEntity);
        // }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Entity otherEntity = other.GetComponent<Entity>();
        // if (otherEntity)
        // {
            // entitiesInRange.RemoveAll(entity => entity.Equals(otherEntity));
        // }
    }

    public abstract void OnDrop();
}