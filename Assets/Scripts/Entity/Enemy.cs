using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public EntityData entity;
    public GameObject player;
    public Vector3 homePosition;

    public float senseRange = 5;
    public float aggressionRange = 3;
    public float attackRange = 1;
    public float damage = 10;

    public float speedNormal = 3;
    public float speedAggressive = 7;
    public float aggressionCooldown = 2;
    public float aggressionTimer;

    public float roamDuration = 30;
    public float roamTimer = 0;
    public bool isRoaming;
    public bool isReturning;
    
    public float distanceChangeCooldown = 5;
    public float distanceChangedTimer;
    public Vector3 targetPosition;

    public float minDistance = 0.1f;
    public Vector3 delta;

    private void Start()
    {
        homePosition = transform.position;
    }

    private void Update()
    {
        LoadPlayer();
        if (player)
        {
            Debug.DrawLine(transform.position,
                transform.position + senseRange * (player.transform.position - transform.position).normalized,
                Color.black, 1);
            Debug.DrawLine(transform.position,
                transform.position + aggressionRange * (player.transform.position - transform.position).normalized,
                Color.blue, 1);
            Debug.DrawLine(transform.position,
                transform.position + attackRange * (player.transform.position - transform.position).normalized,
                Color.green, 1);
            var distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < attackRange)
            {
                if (aggressionTimer < 0)
                {
                    Player.entity.TakeDamage(player.transform, damage);
                    aggressionTimer = aggressionCooldown;
                }
            }
        }

        aggressionTimer -= Time.deltaTime;
    }

    private void LoadPlayer()
    {
        if (!player)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    private void FixedUpdate()
    {
        var distance = float.MaxValue;
        if (player)
        {
            distance = Vector3.Distance(player.transform.position, transform.position);
        }

        distanceChangedTimer -= Time.fixedDeltaTime;

        if (distance < senseRange && !Player.entity.isDead())
        {
            targetPosition = player.transform.position;
        }
        else
        {
            if (distanceChangedTimer < 0)
            {
                targetPosition = transform.position + Random.onUnitSphere;
                targetPosition.z = 0;
                distanceChangedTimer = distanceChangeCooldown;
            }
        }

        Debug.Log(distanceChangedTimer);

        Debug.DrawLine(transform.position, targetPosition, Color.red, 1);

        delta = targetPosition - transform.position;
        if (delta.magnitude > minDistance)
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody)
            {
                rigidbody.AddForce(rigidbody.mass * (distance < aggressionRange ? speedAggressive : speedNormal) *
                                   delta.normalized);
            }
        }
    }
}