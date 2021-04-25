using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WorldTile : MonoBehaviour
{

    public static readonly int POLLUTION_DISPLAY_MIN = 30;
    
    [Range(0, WorldController.MAX_POLLUTION)] public int Pollution;
    public bool AllowPollution;
    
    public GameObject Infection;
    
    private int _lastPollution;
    private GameObject _infectionObject;

    private SpriteRenderer _tileSpriteRenderer;
    
    private void Awake()
    {
        _tileSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetPollution(int pollution)
    {
        Pollution = pollution;
    }
    
    void OnDrawGizmos() 
    {
        if (AllowPollution)
        {
            Handles.Label(transform.position, Pollution.ToString());
        }
    }
    private void Update()
    {
        if (!AllowPollution || _lastPollution == Pollution) return;
        
        SpawnOrDespawnInfection();
        DisableTileSpriteRenderIfPossible();

        if (Pollution >= POLLUTION_DISPLAY_MIN)
        {
            var spriteRenderer = _infectionObject.GetComponent<SpriteRenderer>();
            var color = spriteRenderer.color;
            color.a = (float) Pollution / WorldController.MAX_POLLUTION;
            spriteRenderer.color = color;
        }
    }

    private void SpawnOrDespawnInfection()
    {
        if (Pollution <= POLLUTION_DISPLAY_MIN && _infectionObject != null)
        {
            if (Application.isPlaying)
            {
                Destroy(_infectionObject);
            }
            else
            {
              DestroyImmediate(_infectionObject);  
            }

            _infectionObject = null;
        }
        else if (Pollution >= POLLUTION_DISPLAY_MIN && _infectionObject == null)
        {
            _infectionObject = Instantiate(Infection, transform);
        }
    }

    private void DisableTileSpriteRenderIfPossible()
    {
        if (_tileSpriteRenderer != null)
        {
            if (Pollution == WorldController.MAX_POLLUTION)
            {
                _tileSpriteRenderer.enabled = false;
            }
            else
            {
                _tileSpriteRenderer.enabled = true;
            }
        }
    }
}