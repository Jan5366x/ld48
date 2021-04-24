using System;
using UnityEngine;

[ExecuteInEditMode]
public class GridAlignment : MonoBehaviour
{
    [Range(1, 1000)] public double stepsPerUnitX = 100d;
    [Range(1, 1000)] public double stepsPerUnitY = 100d;

    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (Application.isPlaying)
        {
            enabled = false;
        }
    }

    [ContextMenu("Align")]
    public void Align()
    {
        var position = transform.position;
        position.x = (float) (Math.Round(position.x * stepsPerUnitX) / stepsPerUnitX);
        position.y = (float) (Math.Round(position.y * stepsPerUnitY) / stepsPerUnitY);
        position.z = 0f;

        transform.position = position;
    }

    private void Update()
    {
        if (_spriteRenderer)
        {
            var size = _spriteRenderer.size;
            stepsPerUnitX = 1d / size.x;
            stepsPerUnitY = 1d / size.y;
        }

        Align();
    }
}