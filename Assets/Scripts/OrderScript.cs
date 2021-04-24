using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OrderScript : MonoBehaviour
{
    public float groundOffset;
    
    private const int ChildLayerSpace = 100;
    
    private int _sortingOrder = 0;
    private SpriteRenderer _spriteRenderer;
    private bool _hasOwnRenderer = false;
    
    private void Awake()
    {
        Prepare();
        SetPosition();
    }

    void Update()
    {
        SetPosition();
        
        var objPos = transform.position;
        
        Debug.DrawLine(
            new Vector3(objPos.x + -0.3f,objPos.y + groundOffset), 
            new Vector3(objPos.x + 0.3f,objPos.y + groundOffset),
            _hasOwnRenderer ? Color.yellow : Color.magenta);
    }

    void SetPosition()
    {
        
        // update sprite renderer if in Editor mode to provide live updates
        if(!Application.isPlaying)
            Prepare();
        
        _sortingOrder = (int) -((transform.position.y + groundOffset) * ChildLayerSpace);
        
        if (_hasOwnRenderer)
            _spriteRenderer.sortingOrder = _sortingOrder;
    }

    private void Prepare()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _hasOwnRenderer = _spriteRenderer != null;
    }

    public int GetSortingOrder()
    {
        return _sortingOrder;
    }
}