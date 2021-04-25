using System;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class WorldController : MonoBehaviour
{
    private static Random _random;
    public const int WORLD_SIZE = 10;
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
            int y = (int) o.transform.position.y;

            spawners.Add(Tuple.Create(x, y));
        }

        foreach (var o in GameObject.FindGameObjectsWithTag("Healer"))
        {
            int x = (int) o.transform.position.x;
            int y = (int) o.transform.position.y;

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

                    float infectionProbability = 1f / Math.Abs(i * j);

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
        for (int numTries = 0; numTries < 10; numTries++)
        {
            int x = _random.NextInt(WORLD_SIZE);
            int y = _random.NextInt(WORLD_SIZE);

            if (!IsTileBlocked(x, y))
            {
                spawners.Add(Tuple.Create(x, y));
                WorldTile tile = tiles[x, y];
                tile.PlaceSpawner();
                return;
            }
        }

        Debug.Log("Tried 10 Times to place a new spawner.... No success");
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

    private void Awake()
    {
        _random = new Random((uint) (1337 * (Int32) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds));
        InitializeTiles();
    }


    private void Update()
    {
        infectionSpreadTime -= Time.deltaTime;
        spawnerPlaceTime -= Time.deltaTime;

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