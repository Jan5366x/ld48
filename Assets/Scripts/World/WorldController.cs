using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = Unity.Mathematics.Random;

public class WorldController : MonoBehaviour
{
    private static Random _random;
    public const int POLLUTION_DISPLAY_MIN = 30;
    public const int WORLD_SIZE = 20;
    public const int MAX_POLLUTION = 255;
    private static WorldTile[,] tiles;
    private static bool[,] buildable;
    private static bool[,] pollutable;
    private static int[,] pollution;
    private static List<Tuple<int, int>> spawners;
    private static List<Tuple<int, int>> healers;

    public float spawnerPlaceDuration = 30;
    public float spawnerPlaceTime = 30;

    public float infectionSpreadDuration = 2;
    public float infectionSpreadTime = 0;

    public float coinSpreadDuration = 2;
    public float coinSpreadTime = 0;

    public GameObject spawnerPrefab;
    public GameObject coinPrefab;
    public GameObject healingCrystalPrefab;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _random = new Random((uint) (1337 * (Int32) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds));
        InitializeTiles();
    }

    public void InitializeTiles()
    {
        tiles = new WorldTile[WORLD_SIZE, WORLD_SIZE];
        buildable = new bool[WORLD_SIZE, WORLD_SIZE];
        pollutable = new bool[WORLD_SIZE, WORLD_SIZE];
        pollution = new int[WORLD_SIZE, WORLD_SIZE];
        spawners = new List<Tuple<int, int>>();
        healers = new List<Tuple<int, int>>();

        for (int x = 0; x < WORLD_SIZE; x++)
        {
            for (int y = 0; y < WORLD_SIZE; y++)
            {
                pollution[x, y] = 0;
                pollutable[x, y] = false;
                buildable[x, y] = false;

                GameObject ground = GameObject.Find("G-" + x + "-" + y);
                if (ground)
                {
                    WorldTile tile = ground.GetComponent<WorldTile>();
                    buildable[x, y] = tile;
                    if (tile)
                    {
                        tiles[x, y] = tile;
                        pollutable[x, y] = tile.AllowPollution;
                        pollution[x, y] = tile.Pollution;
                    }
                }
            }
        }

        foreach (var o in GameObject.FindGameObjectsWithTag("Spawner"))
        {
            int x = (int) o.transform.position.x;
            int y = (int) -o.transform.position.y;

            spawners.Add(Tuple.Create(x, y));
        }

        foreach (var o in GameObject.FindGameObjectsWithTag("Healer"))
        {
            int x = (int) o.transform.position.x;
            int y = (int) -o.transform.position.y;

            healers.Add(Tuple.Create(x, y));
        }
    }

    private void SpreadInfection()
    {
        foreach (var spawner in spawners)
        {
            for (int i = -10; i <= 10; i++)
            {
                for (int j = -10; j <= 10; j++)
                {
                    int xx = spawner.Item1 + i;
                    int yy = spawner.Item2 + j;

                    if (xx < 0 || yy < 0 || xx >= WORLD_SIZE || yy >= WORLD_SIZE) continue;

                    float infectionProbability = 1f / (Math.Abs(i) + Math.Abs(j));

                    if (_random.NextFloat() < infectionProbability)
                    {
                        pollution[xx, yy] = Math.Min(MAX_POLLUTION,
                            pollution[xx, yy] + (20 - Math.Abs(i) - Math.Abs(j)));
                    }
                }
            }
        }
    }

    private void SpreadHealing()
    {
        foreach (var healer in healers)
        {
            for (int i = -10; i <= 10; i++)
            {
                for (int j = -10; j <= 10; j++)
                {
                    int xx = healer.Item1 + i;
                    int yy = healer.Item2 + j;

                    if (xx < 0 || yy < 0 || xx >= WORLD_SIZE || yy >= WORLD_SIZE) continue;

                    if (i == 0 && j == 0)
                    {
                        pollution[xx, yy] = 0;
                    }
                    else
                    {
                        pollution[xx, yy] = Math.Max(0, pollution[xx, yy] - 20);
                    }
                }
            }
        }
    }

    private void PlaceSpawner()
    {
        List<Tuple<int, int>> spawnableSpaces = new List<Tuple<int, int>>();
        for (int x = 0; x < WORLD_SIZE; x++)
        {
            for (int y = 0; y < WORLD_SIZE; y++)
            {
                if (pollution[x, y] > 0 && !IsTileBlocked(x, y))
                {
                    spawnableSpaces.Add(Tuple.Create(x, y));
                }
            }
        }

        if (spawnableSpaces.Count == 0)
        {
            Debug.Log("You win the game!");
            return;
        }

        Tuple<int, int> space = spawnableSpaces[_random.NextInt(spawnableSpaces.Count)];
        spawners.Add(space);
        WorldTile tile = tiles[space.Item1, space.Item2];
        Instantiate(spawnerPrefab, tile.transform);
    }

    public static bool DeleteSpawner(int x, int y)
    {
        for (var i = spawners.Count - 1; i >= 0; i--)
        {
            if (spawners[i].Item1 == x && spawners[i].Item2 == y)
            {
                spawners.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public static bool PlaceHealer(int x, int y)
    {
        if (IsTileBlocked(x, y))
        {
            return false;
        }

        healers.Add(Tuple.Create<int, int>(x, y));
        return true;
    }

    private static bool IsTileBlocked(int x, int y)
    {
        if (x < 0 || y < 0 || x > WORLD_SIZE || y > WORLD_SIZE)
        {
            return true;
        }

        if (!buildable[x, y])
        {
            return true;
        }

        foreach (var spawner in spawners)
        {
            if (spawner.Item1 == x && spawner.Item2 == y)
            {
                return true;
            }
        }

        foreach (var healer in healers)
        {
            if (healer.Item1 == x && healer.Item2 == y)
            {
                return true;
            }
        }

        return false;
    }

    private void Update()
    {
        infectionSpreadTime -= Time.deltaTime;
        spawnerPlaceTime -= Time.deltaTime;
        coinSpreadTime -= Time.deltaTime;

        if (infectionSpreadTime < 0)
        {
            SpreadInfection();
            SpreadHealing();

            for (int x = 0; x < WORLD_SIZE; x++)
            {
                for (int y = 0; y < WORLD_SIZE; y++)
                {
                    WorldTile tile = tiles[x, y];
                    if (tile)
                    {
                        tile.SetPollution(pollution[x, y]);
                    }
                }
            }

            infectionSpreadTime = infectionSpreadDuration;
        }

        if (spawnerPlaceTime < 0)
        {
            PlaceSpawner();
            spawnerPlaceTime = spawnerPlaceDuration;
        }

        if (coinSpreadTime < 0)
        {
            SpreadCoin();
            coinSpreadTime = coinSpreadDuration;
        }

        // TODO FIXME 
        //GameOverHandler.victoryCondition = CheckVictoryCondition();
        //Debug.Log(GameOverHandler.victoryCondition);
    }

    private bool CheckVictoryCondition()
    {
        for (int x = 0; x < WORLD_SIZE; x++)
        {
            for (int y = 0; y < WORLD_SIZE; y++)
            {
                if (pollutable[x, y])
                {
                    if (pollution[x, y] > POLLUTION_DISPLAY_MIN)
                    {
                        Debug.DrawLine(new Vector3(x, -y, 0), new Vector3(x + 1, -y - 1, 0), Color.red, 1f);
                        return false;
                    }
                }
            }
        }

        foreach (var spawner in spawners)
        {
            Debug.Log(spawner);
            Debug.DrawLine(new Vector3(spawner.Item1, -spawner.Item2, 0),
                new Vector3(spawner.Item1 + 1, -spawner.Item2 - 1, 0), Color.green, 1f);
        }

        Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(spawners.Count, -spawners.Count, 0), Color.green, 1f);
        return spawners.Count == 0;
    }

    private void SpreadCoin()
    {
        for (int i = 0; i < 100; i++)
        {
            bool isCoin = _random.NextBool();
            int x = _random.NextInt(WORLD_SIZE);
            int y = _random.NextInt(WORLD_SIZE);

            if (pollution[x, y] > POLLUTION_DISPLAY_MIN || IsTileBlocked(x, y))
            {
                continue;
            }

            WorldTile tile = tiles[x, y];
            if (tile)
            {
                var delta = new Vector3(_random.NextFloat(-0.3f, 0.3f), _random.NextFloat(-0.3f, 0.3f), 0);
                Instantiate(isCoin ? coinPrefab : healingCrystalPrefab, tile.transform.position + delta,
                    Quaternion.identity);
                return;
            }
        }
    }

    public static Tuple<int, int> PositionByName(String name)
    {
        if (name.StartsWith("G-"))
        {
            var parts = name.Split('-');
            return Tuple.Create(int.Parse(parts[1]), int.Parse(parts[2]));
        }

        return null;
    }
}