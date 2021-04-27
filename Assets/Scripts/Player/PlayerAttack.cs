using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    public GameObject healerPrefab;
    public GameObject healerPlaceFailedPrefab;
    public GameObject defenseTowerPrefab;
    public GameObject defenseTowerPlaceFailedPrefab;

    public float attackCooldown = 0.5f;
    public float attackTimer = 0.5f;

    void Update()
    {
        attackTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Attack"))
        {
            if (attackTimer < 0)
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

                attackTimer = attackCooldown;
                transform.Find("PlayerBody/Weapon").GetComponent<Animator>().SetTrigger("Trigger");
                RandomizedSound.Play(transform, RandomizedSound.ATTACK);
            }
        }

        if (Input.GetButtonDown("PlaceHealer"))
        {
            PlaceHealer();
        }

        if (Input.GetButtonDown("PlaceDefenseTower"))
        {
            PlaceDefenseTower();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void PlaceHealer()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);

        if (Player.numMoney >= 5)
        {
            if (WorldController.PlaceHealer(x, -y))
            {
                Instantiate(healerPrefab, new Vector3(x, y, 0f), Quaternion.identity);
                Player.UseMoney(5);
                return;
            }
        }

        Instantiate(healerPlaceFailedPrefab, new Vector3(x, y, 0f), Quaternion.identity);
    }

    private void PlaceDefenseTower()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);

        if (Player.numMoney >= 5)
        {
            if (WorldController.PlaceDefenseTower(x, -y))
            {
                Instantiate(defenseTowerPrefab, new Vector3(x, y, 0f), Quaternion.identity);
                Player.UseMoney(5);
                return;
            }
        }

        Instantiate(defenseTowerPlaceFailedPrefab, new Vector3(x, y, 0f), Quaternion.identity);
    }
}