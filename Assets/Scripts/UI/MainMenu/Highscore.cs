using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    public GameObject TemplateHighscoreItem;

    public GameObject PlayerHighscoreContent;

    private (string, long)[] _test = 
    {
        ("Player 1", 11111111111),
        ("Player 2", 22222222222),
        ("Player 3", 33333333333),
        ("Player 4", 44444444444),
    };

// Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (var valueTuple in this._test)
        {
            Debug.Log($"ADD Item {i}");
            
            var hs = Instantiate(this.TemplateHighscoreItem, this.PlayerHighscoreContent.transform, true);
            hs.SetActive(true);
            var hsi = hs.GetComponent<HighscoreItem>();
            hsi.SetValues(valueTuple.Item1, valueTuple.Item2);

            var posY = i * 30 * -1;
            Debug.Log($"POS Y: {posY}");
            hs.transform.position = new Vector3(0, posY, 0);
            
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
