using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyFade : MonoBehaviour
{
    public float minOpacity = 0.3f;
    public float fadeOutDuration = 0.5f;
    public float fadeInDuration = 0.5f;
    
    private bool _doFade;
    private bool _fadeOut;
    private float _fadeTime;
    private float _fadeValueStart;
    private SpriteRenderer _spriteRenderer;
    private void Start() => _spriteRenderer = GetComponent<SpriteRenderer>();

    public void Update()
    {
        if (!_doFade) return;
        
        var color = _spriteRenderer.color;
        
        if (_fadeOut)
        {
            if (_fadeTime > fadeOutDuration)
            {
                color.a = minOpacity;
                _doFade = false;
            }

            color.a = Mathf.Lerp(_fadeValueStart, minOpacity, _fadeTime / fadeOutDuration);
        }
        else
        {
            if (_fadeTime > fadeInDuration)
            {
                color.a = 1;
                _doFade = false;
            }

            color.a = Mathf.Lerp(_fadeValueStart, 1, _fadeTime / fadeInDuration);
        }

        _spriteRenderer.color = color;
        _fadeTime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponentInParent<Player>()) return;
        
        _fadeTime = 0;
        _fadeValueStart = GetComponent<SpriteRenderer>().color.a;
        _doFade = true;
        _fadeOut = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.GetComponentInParent<Player>()) return;
        
        _fadeTime = 0;
        _fadeValueStart = GetComponent<SpriteRenderer>().color.a;
        _doFade = true;
        _fadeOut = false;
    }
}