using System;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class WorldController : MonoBehaviour
{
    private Random _random;
    public const int WORLD_SIZE = 3;
    private const int MAX_POLLUTION = 255;
    private WorldTile[,] tiles;
    private bool[,] buildable;
    private int[,] pollution;
    private List<Tuple<int, int>> spawners;
    private List<Tuple<int, int>> healers;

    public float spawnerPlaceDuration = 30;
    public float spawnerPlaceTime = 30;

    public float infectionSpreadDuration = 2;
    public float infectionSpreadTime = 0;

    public void InitializeTiles()
    {
        tiles = new WorldTile[WORLD_SIZE, WORLD_SIZE];
        buildable = new bool[WORLD_SIZE, WORLD_SIZE];
        pollution = new int[WORLD_SIZE, WORLD_SIZE];
        spawners = new List<Tuple<int, int>>();
        healers = new List<Tuple<int, int>>();

        for (int x = 0; x < WORLD_SIZE; x++)
        {
            for (int y = 0; y < WORLD_SIZE; y++)
            {
                GameObject ground = GameObject.Find("G-" + x + "-" + y);
                if (ground)
                {
                    WorldTile tile = ground.GetComponent<WorldTile>();
                    if (tile)
                    {
                        tiles[x, y] = tile;
                    }

                    buildable[x, y] = true;
                    pollution[x, y] = 0;
                }
            }
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
                    int yy = spawner.Item2 + i;

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
                    int yy = healer.Item2 + i;

                    if (xx < 0 || yy < 0 || xx >= WORLD_SIZE || yy >= WORLD_SIZE) continue;

                    pollution[xx, yy] = Math.Max(0, pollution[xx, yy] - 20);
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
                return;
            }
        }

        Debug.Log("Tried 10 Times to place a new spawner.... No success");
    }

    public bool DeleteSpawner(int x, int y)
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

    public bool PlaceHealer(int x, int y)
    {
        if (IsTileBlocked(x, y))
        {
            return false;
        }

        healers.Add(Tuple.Create<int, int>(x, y));
        return true;
    }

    private bool IsTileBlocked(int x, int y)
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
                    Debug.Log(x + " " + y + ": " + pollution[x, y] + " " + IsTileBlocked(x, y));

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
}