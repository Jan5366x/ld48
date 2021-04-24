using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float durationSeconds;
    
    void Start() => Destroy(gameObject, durationSeconds);
}