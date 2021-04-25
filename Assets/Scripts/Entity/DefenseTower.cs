using System.Collections.Generic;
using UnityEngine;

public class DefenseTower : MonoBehaviour
{
    public List<Enemy> enemiesInRange = new List<Enemy>();

    public GameObject projectilePrefab;
    
    public float aggressionCoolDown = 5;
    public float aggressionTimer = 0;

    private void Update()
    {
        aggressionTimer -= Time.deltaTime;
        if (aggressionTimer < 0)
        {
            if (enemiesInRange.Count > 0)
            {
                Enemy enemy = enemiesInRange[Random.Range(0, enemiesInRange.Count)];
                if (enemy)
                {
                    var projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                    var projectile = projectileObject.GetComponent<Projectile>();
                    projectile.target = enemy.gameObject;
                    aggressionTimer = aggressionCoolDown;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            enemiesInRange.Remove(enemy);
        }
    }
}