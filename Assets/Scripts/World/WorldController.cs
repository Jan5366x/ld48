using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = Unity.Mathematics.Random;

public class WorldController : MonoBehaviour
{
    private static Random _random;
    public const int POLLUTION_DISPLAY_MIN = 30;
    public const int SPAWNER_POLLUTION_MIN = 500;
    public const int POLLUTION_HEALER_MAX = 1500;
    public const int POLLUTION_TOWER_MAX = 5000;
    public const int WORLD_SIZE = 100;
    public const int MAX_POLLUTION = 10000;
    private static WorldTile[,] tiles;
    private static bool[,] buildable;
    private static bool[,] pollutable;
    private static int[,] pollution;
    private static List<Tuple<int, int>> spawners;
    private static List<Tuple<int, int>> healers;
    private static List<Tuple<int, int>> towers;
    public static float infectionStatus;

    public int passiveRemoval = 10;
    public int healerRemoval = 80;
    public int spawnerAddingMin = 40;
    public int spawnerAddingMax = 120;
    public float spawnerPlaceDuration = 30;
    public float spawnerPlaceTime = 30;
    public int healerRadius = 5;
    public int spawnerRadius = 10;

    public float infectionSpreadDuration = 0.2f;
    public float infectionSpreadTime = 0;

    public float coinSpreadDuration = 5;
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
                    if (tile)
                    {
                        tiles[x, y] = tile;
                        buildable[x, y] = tile.AllowPollution;
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
            for (int i = -spawnerRadius; i <= spawnerRadius; i++)
            {
                for (int j = -spawnerRadius; j <= spawnerRadius; j++)
                {
                    int xx = spawner.Item1 + i;
                    int yy = spawner.Item2 + j;

                    if (xx < 0 || yy < 0 || xx >= WORLD_SIZE || yy >= WORLD_SIZE) continue;

                    var distance = Math.Abs(i) + Math.Abs(j);
                    float infectionProbability = 1f / distance;

                    if (_random.NextFloat() < infectionProbability)
                    {
                        int strength = (int) Mathf.Lerp(spawnerAddingMax, spawnerAddingMin, distance / 20f);
                        pollution[xx, yy] = Math.Min(MAX_POLLUTION, pollution[xx, yy] + strength);
                    }
                }
            }
        }
    }

    private void SpreadHealing()
    {
        foreach (var healer in healers)
        {
            for (int i = -healerRadius; i <= healerRadius; i++)
            {
                for (int j = -healerRadius; j <= healerRadius; j++)
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
                        pollution[xx, yy] = Math.Max(0, pollution[xx, yy] - healerRemoval);
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
                if (pollution[x, y] > SPAWNER_POLLUTION_MIN && !IsTileBlocked(x, y))
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
        ClearCoveringObjects(space.Item1, space.Item2);

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
        if (IsTileBlocked(x, y) || pollution[x, y] > POLLUTION_HEALER_MAX)
        {
            return false;
        }

        ClearCoveringObjects(x, y);
        healers.Add(Tuple.Create<int, int>(x, y));
        return true;
    }

    public static bool DeleteHealer(int x, int y)
    {
        for (var i = healers.Count - 1; i >= 0; i--)
        {
            if (healers[i].Item1 == x && healers[i].Item2 == y)
            {
                healers.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public static bool PlaceDefenseTower(int x, int y)
    {
        if (IsTileBlocked(x, y) || pollution[x, y] > POLLUTION_TOWER_MAX)
        {
            return false;
        }

        ClearCoveringObjects(x, y);
        towers.Add(Tuple.Create(x, y));
        return true;
    }

    public static bool DeleteDefenseTower(int x, int y)
    {
        for (var i = towers.Count - 1; i >= 0; i--)
        {
            if (towers[i].Item1 == x && towers[i].Item2 == y)
            {
                towers.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    private static bool IsTileBlocked(int x, int y)
    {
        if (x < 0 || y < 0 || x >= WORLD_SIZE || y >= WORLD_SIZE)
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

        if (GameOverHandler.gameOver)
        {
            return;
        }


        if (infectionSpreadTime < 0)
        {
            long pollutableTiles = 0;
            long pollutedTiles = 0;
            SpreadInfection();
            SpreadHealing();
            PassiveHealing();

            for (int x = 0; x < WORLD_SIZE; x++)
            {
                for (int y = 0; y < WORLD_SIZE; y++)
                {
                    if (pollutable[x, y])
                    {
                        pollutableTiles++;
                    }

                    if (pollution[x, y] > POLLUTION_DISPLAY_MIN)
                    {
                        pollutedTiles++;
                    }

                    WorldTile tile = tiles[x, y];
                    if (tile)
                    {
                        tile.SetPollution(pollution[x, y]);
                    }
                }
            }

            infectionSpreadTime = infectionSpreadDuration;
            infectionStatus = pollutedTiles / (float) pollutableTiles;
            GameOverHandler.victoryCondition = CheckVictoryCondition();
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
    }

    private void PassiveHealing()
    {
        for (int x = 0; x < WORLD_SIZE; x++)
        {
            for (int y = 0; y < WORLD_SIZE; y++)
            {
                pollution[x, y] = Mathf.Max(0, pollution[x, y] - passiveRemoval);
            }
        }
    }

    private bool CheckVictoryCondition()
    {
        foreach (var spawner in spawners)
        {
            Debug.Log(spawner);
            Debug.DrawLine(new Vector3(spawner.Item1, -spawner.Item2, 0),
                new Vector3(spawner.Item1 + 1, -spawner.Item2 - 1, 0), Color.magenta, 1f);
        }

        bool result = true;

        for (int x = 0; x < WORLD_SIZE; x++)
        {
            for (int y = 0; y < WORLD_SIZE; y++)
            {
                if (buildable[x, y])
                {
                    Debug.DrawLine(new Vector3(x, -y, 0), new Vector3(x + 1, -y, 0), Color.blue, 1f);
                }

                if (pollutable[x, y])
                {
                    Debug.DrawLine(new Vector3(x, -y, 0), new Vector3(x, -y - 1, 0), Color.green, 1f);
                    if (pollution[x, y] > POLLUTION_DISPLAY_MIN)
                    {
                        Debug.DrawLine(new Vector3(x, -y, 0), new Vector3(x + 1, -y - 1, 0), Color.red, 1f);
                        result = false;
                    }
                }
            }
        }

        return result && spawners.Count == 0;
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

    private static void ClearCoveringObjects(int x, int y)
    {
        WorldTile tile = tiles[x, y];
        if (tile)
        {
            tile.ClearCoveringObjects();
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