using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EntityData entity;
    public GameObject player;
    public Vector3 homePosition;
    public float maxHomeDistance = 10;
    public float minHomeDistance = 1;

    public float senseRange = 5;
    public float aggressionRange = 3;
    public float attackRange = 1;
    public float damage = 10;

    public float speedNormal = 3;
    public float speedAggressive = 7;
    public float aggressionCooldown = 2;
    public float aggressionTimer;

    public float roamRange = 3; 
    public float roamDuration = 30;
    public float roamTimer = 0;
    public float roamStartDuration = 10;
    public float roamStartTimer = 0;
    public bool isRoaming;
    public bool isReturning;

    public float roamTargetChangedCooldown = 3;
    public float roamTargetChangedTimer;
    public Vector3 targetPosition;

    public float minDistance = 0.1f;
    public Vector3 delta;

    private void Start()
    {
        homePosition = transform.position;
    }

    private void SelectTargetPosition()
    {
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);
        float homeDistance = Vector3.Distance(homePosition, transform.position);

        if (playerDistance < senseRange)
        {
            targetPosition = player.transform.position;
            return;
        }

        if (homeDistance < minHomeDistance)
        {
            if (isReturning)
            {
                roamStartTimer = roamStartDuration;
                isReturning = false;
                isRoaming = false;
            }

            if (roamStartTimer < 0 && !isRoaming)
            {
                roamTimer = roamDuration;
                isRoaming = true;
            }
        }

        if (homeDistance > maxHomeDistance || (isRoaming && roamTimer < 0))
        {
            isRoaming = false;
            isReturning = true;
        }

        if (isReturning)
        {
            targetPosition = homePosition;
        }

        if (isRoaming)
        {
            if (roamTargetChangedTimer < 0)
            {
                targetPosition = transform.position + Random.onUnitSphere * roamRange;
                targetPosition.z = 0;
                roamTargetChangedTimer = roamTargetChangedCooldown;
            }
        }
    }

    private void Update()
    {
        aggressionTimer -= Time.deltaTime;
        roamTimer -= Time.deltaTime;
        roamTargetChangedTimer -= Time.deltaTime;
        roamStartTimer -= Time.deltaTime;

        LoadPlayer();
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);

        if (playerDistance < attackRange)
        {
            if (aggressionTimer < 0)
            {
                Player.entity.TakeDamage(player.transform, damage);
                aggressionTimer = aggressionCooldown;
            }
        }

        SelectTargetPosition();
        DrawDebugLines();

        delta = targetPosition - transform.position;
        if (delta.magnitude > minDistance)
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody)
            {
                rigidbody.AddForce(rigidbody.mass * (playerDistance < aggressionRange ? speedAggressive : speedNormal) *
                                   delta.normalized);
            }
        }
    }

    private void DrawDebugLines()
    {
        Debug.DrawLine(transform.position, targetPosition, Color.red, 1);
        Debug.DrawLine(transform.position, homePosition, Color.magenta, 1);
        Vector3 playerDirection = (player.transform.position - transform.position).normalized;
        Debug.DrawLine(transform.position, transform.position + senseRange * playerDirection, Color.black, 1);
        Debug.DrawLine(transform.position, transform.position + aggressionRange * playerDirection, Color.blue, 1);
        Debug.DrawLine(transform.position, transform.position + attackRange * playerDirection, Color.green, 1);
    }

    private void LoadPlayer()
    {
        if (!player)
        {
            player = GameObject.FindWithTag("Player");
        }
    }
}