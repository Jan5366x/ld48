using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public SpawnerWave[] waves;
    public bool playerInRange;
    public float spawnTimer = 0;

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0 && playerInRange)
        {
            var spawnerWave = waves[Random.Range(0, waves.Length)];
            SpawnAround(spawnerWave);
            spawnTimer = Random.Range(spawnerWave.delayMin, spawnerWave.delayMax);
        }
    }

    void SpawnAround(SpawnerWave wave)
    {
        int count = Random.Range(wave.countMin, wave.countMax);
        for (int i = 0; i < count; i++)
        {
            var position = transform.position;
            var range = wave.spawnerRange;
            float spawnX = Random.Range(position.x - range.x, position.x + range.x);
            float spawnY = Random.Range(position.y - range.y, position.y + range.y);

            Instantiate(wave.prefab, new Vector3(spawnX, spawnY), Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            playerInRange = true;
            AnimationHelper.SetParameter(transform.parent.GetComponent<Animator>(), "Active", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            playerInRange = false;
            AnimationHelper.SetParameter(transform.parent.GetComponent<Animator>(), "Active", false);
        }
    }
}