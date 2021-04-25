using System;
using UnityEngine;

[Serializable]
public class SpawnerWave
{
    public float delayMin;
    public float delayMax;
    public int countMin;
    public int countMax;
    public Transform prefab;
    public Vector2 spawnerRange;
}