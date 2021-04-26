using UnityEditor;
using UnityEngine;

public class WorldTile : MonoBehaviour
{

    [Range(0, WorldController.MAX_POLLUTION)]
    public int Pollution;

    public bool AllowPollution = false;

    public GameObject Infection;

    private int _lastPollution;
    private GameObject _infectionObject;

    private SpriteRenderer _tileSpriteRenderer;
    private PollutableObject[] _pollutableObjects;

    private void Start()
    {
        _tileSpriteRenderer = GetComponent<SpriteRenderer>();
        DiscoverPollutableObjects();
    }

    public void DiscoverPollutableObjects()
    {
        _pollutableObjects = GetComponentsInChildren<PollutableObject>();
        UpdatePollutableObjects();
    }

    public void SetPollution(int pollution)
    {
        _lastPollution = Pollution;
        Pollution = pollution;
    }

    private void Update()
    {
        if (!AllowPollution || _lastPollution == Pollution) return;
        
        UpdatePollutableObjects();
        SpawnOrDespawnInfection();
        DisableTileSpriteRenderIfPossible();

        if (_infectionObject)
        {
            var spriteRenderer = _infectionObject.GetComponent<SpriteRenderer>();
            var color = spriteRenderer.color;
            color.a = (float) Pollution / WorldController.MAX_POLLUTION;
            spriteRenderer.color = color;
        }
    }

    private void UpdatePollutableObjects()
    {
        foreach (var pollutableObject in _pollutableObjects)
        {
            pollutableObject.SetPolluted(Pollution > WorldController.POLLUTION_DISPLAY_MIN);
        }
    }

    private void SpawnOrDespawnInfection()
    {
        if (Pollution <= WorldController.POLLUTION_DISPLAY_MIN)
        {
            if (_infectionObject)
            {
                if (Application.isPlaying)
                {
                    Destroy(_infectionObject);
                }
                else
                {
                    DestroyImmediate(_infectionObject);
                }

                _infectionObject = null;
            }
        }
        else
        {
            if (!_infectionObject)
            {
                _infectionObject = Instantiate(Infection, transform);
            }
        }
    }

    private void DisableTileSpriteRenderIfPossible()
    {
        if (_tileSpriteRenderer)
        {
            if (Pollution >= WorldController.MAX_POLLUTION)
            {
                _tileSpriteRenderer.enabled = false;
            }
            else
            {
                _tileSpriteRenderer.enabled = true;
            }
        }
    }

    // clears not building objects which are covering the world tile (like trees / small stones)
    public void ClearCoveringObjects()
    {
        foreach (var pollutableObject in _pollutableObjects)
        {
            pollutableObject.DestroyObject();
        }
        _pollutableObjects = new PollutableObject[0];
    }
    
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (AllowPollution)
        {
            Handles.Label(transform.position, Pollution.ToString());
        }
    }
#endif
}