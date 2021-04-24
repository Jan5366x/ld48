using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderScript : MonoBehaviour
{
    public float offset;
    private SpriteRenderer _spriteRenderer;
    
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetPosition();
    }

    void Update() => SetPosition();

    void SetPosition() => _spriteRenderer.sortingOrder = (int)-((transform.position.y + offset) * 100);
}
