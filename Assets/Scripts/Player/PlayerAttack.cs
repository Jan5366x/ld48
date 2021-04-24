using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.up * 2);
            Debug.DrawLine(transform.position, transform.position + Vector3.down * 2);
            Debug.DrawLine(transform.position, transform.position + Vector3.left * 2);
            Debug.DrawLine(transform.position, transform.position + Vector3.right * 2);
            var overlapCircleAll = Physics2D.OverlapCircleAll(transform.position, 2);
            Debug.Log(overlapCircleAll);
            foreach (var collider in overlapCircleAll)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy)
                {
                    enemy.entity.TakeDamage(collider.transform, 10f);
                }
            }
        }
    }
}