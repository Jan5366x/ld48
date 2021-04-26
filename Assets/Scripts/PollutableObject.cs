using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this shitty fucker currently has no function without child objects ... time is running out to improve this ....
public class PollutableObject : MonoBehaviour
{
    private PollutableChildObject[] _allChildren;
    private bool _lastValue;
    
    void Start()
    {
        // discover child objects
        _allChildren = GetComponentsInChildren<PollutableChildObject>();
    }

    public void SetPolluted(bool value)
    {
        if (_lastValue == value) return;
        
        foreach (var pollutableChildObject in _allChildren)
        {
            pollutableChildObject.SetPolluted(value);
        }

        _lastValue = value;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
