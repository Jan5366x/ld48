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
        Animator animator = transform.parent.GetComponent<Animator>();
        if (enemiesInRange.Count == 0)
        {
            aggressionTimer = aggressionCoolDown;
            AnimationHelper.SetParameter(animator, "Idle", true);
            AnimationHelper.SetParameter(animator, "AggressionStep", 0);
        }
        else
        {
            var inverseLerp = Mathf.InverseLerp(0, aggressionCoolDown, aggressionTimer);
            var lerp = Mathf.Lerp(4, 0, inverseLerp);
            int aggressionStep = (int) lerp;

            AnimationHelper.SetParameter(animator, "Idle", false);
            AnimationHelper.SetParameter(animator, "AggressionStep", aggressionStep);

            aggressionTimer -= Time.deltaTime;
            if (aggressionTimer < 0)
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