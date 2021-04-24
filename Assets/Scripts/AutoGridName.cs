using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AutoGridName : MonoBehaviour
{
    private Vector3 _lastLocation;
    
    public void Awake () => AutoName();

    public void Update () => AutoName();

    private void AutoName()
    {
        if (transform.position.Equals(_lastLocation)) return;
        
        var position = transform.position;
        name = "G-" + (int)position.x + "-" + (int)-position.y;
        _lastLocation = new Vector3(position.x, position.y, position.z);
    }
}
