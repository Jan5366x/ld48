using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public Vector3 moveVector = Vector3.up;
    public void Update() => transform.position += moveVector * Time.deltaTime;
}