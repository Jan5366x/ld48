using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    public GameObject PlayerHighscoreItem1;
    public GameObject PlayerHighscoreItem2;
    public GameObject PlayerHighscoreItem3;
    public GameObject PlayerHighscoreItem4;
    public GameObject PlayerHighscoreItem5;

    private (string, long)[] _test = 
    {
        ("Player 1", 11111111111),
        ("Player 2", 22222222222),
        ("Player 3", 33333333333),
        ("Player 4", 44444444444),
        ("Player 5", 44444444444),
    };
    
    void Start()
    {
        // int i = 0;
        // foreach (var valueTuple in this._test)
        // {
        //     Debug.Log($"ADD Item {i}");
        //     
        //     var hs = Instantiate(this.TemplateHighscoreItem, this.PlayerHighscoreContent.transform, true);
        //     hs.SetActive(true);
        //     var hsi = hs.GetComponent<HighscoreItem>();
        //     hsi.SetValues(valueTuple.Item1, valueTuple.Item2);
        //
        //     var posY = i * 30 * -1;
        //     Debug.Log($"POS Y: {posY}");
        //     hs.transform.position = new Vector3(0, posY, 0);
        //     
        //     i++;
        // }

        this.SetValues(this._test[0], this.PlayerHighscoreItem1);
        this.SetValues(this._test[1], this.PlayerHighscoreItem2);
        this.SetValues(this._test[2], this.PlayerHighscoreItem3);
        this.SetValues(this._test[3], this.PlayerHighscoreItem4);
        this.SetValues(this._test[4], this.PlayerHighscoreItem5);
    }

    private void SetValues((string, long) item, GameObject gameObject)
    {
        var hsi = gameObject.GetComponent<HighscoreItem>(); 
        hsi.SetValues(item.Item1, item.Item2);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
