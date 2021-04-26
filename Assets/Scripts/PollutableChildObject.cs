using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollutableChildObject : MonoBehaviour
{
    public Sprite pollutedSprite;
    private Sprite _normalSprite;
    
    private SpriteRenderer _spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _normalSprite = _spriteRenderer.sprite;
    }

    public void SetPolluted(bool value)
    {
        _spriteRenderer.sprite = value ? pollutedSprite : _normalSprite;
    }
}
