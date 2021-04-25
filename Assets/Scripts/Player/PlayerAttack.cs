using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject healerPrefab;
    public GameObject defenseTowerPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.up * 2, Color.black, 1f);
            Debug.DrawLine(transform.position, transform.position + Vector3.down * 2, Color.black, 1f);
            Debug.DrawLine(transform.position, transform.position + Vector3.left * 2, Color.black, 1f);
            Debug.DrawLine(transform.position, transform.position + Vector3.right * 2, Color.black, 1f);
            var overlapCircleAll = Physics2D.OverlapCircleAll(transform.position, 2);
            foreach (var collider in overlapCircleAll)
            {
                if (collider.isTrigger) continue;
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy)
                {
                    enemy.entity.TakeDamage(collider.transform, 10f);
                }
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            int x = Mathf.RoundToInt(transform.position.x);
            int y = Mathf.RoundToInt(transform.position.y);

            if (Player.numMoney >= 5)
            {
                if (WorldController.PlaceHealer(x, -y))
                {
                    Debug.Log("Placing Spawner at " + x + " " + y);
                    Instantiate(healerPrefab, new Vector3(x, y, 0f), Quaternion.identity);
                    Player.UseMoney(5);
                }
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            int x = Mathf.RoundToInt(transform.position.x);
            int y = Mathf.RoundToInt(transform.position.y);

            if (Player.numMoney >= 5)
            {
                Instantiate(defenseTowerPrefab, new Vector3(x, y, 0f), Quaternion.identity);
                Player.UseMoney(5);
            }
        }
    }
}