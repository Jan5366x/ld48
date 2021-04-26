using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public float minOpacity;
    public float fadeOutDuration = 3f;
    

    private float _fadeTime;
    private float _fadeValueStart;
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _fadeValueStart = _spriteRenderer.color.a;
    }

    public void Update()
    {
        var color = _spriteRenderer.color;
        
        if (_fadeTime > fadeOutDuration)
        {
            color.a = minOpacity;
            Destroy(this);
            return;
        }

        color.a = Mathf.Lerp(_fadeValueStart, minOpacity, _fadeTime / fadeOutDuration);
        _spriteRenderer.color = color;
        _fadeTime += Time.deltaTime;
    }
}
