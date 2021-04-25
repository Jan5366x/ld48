using System;
using UnityEngine;

public class PlayerDieTrigger : MonoBehaviour
{
    private void Start()
    {
        GameObject.FindWithTag("MainCamera").GetComponent<GameOverHandler>().OnDeath();
    }
}