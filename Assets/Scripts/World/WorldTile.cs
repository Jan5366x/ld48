using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{

    public int Pollution;
    public GameObject Infection;

    private int _lastPollution;
    
    private void Awake()
    {
    }

    public void SetPollution(int pollution)
    {
        Pollution = pollution;
    }

    private void Update()
    {
        if (_lastPollution != Pollution)
        {
            
        }
    }
}