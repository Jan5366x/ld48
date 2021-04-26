using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectForTestScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var speed = 100f;
        var moveX = this.gameObject.transform.position.x; 
        var moveY = this.gameObject.transform.position.y; 
        if (Input.GetKey(KeyCode.A))
        {
            moveX -= Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX += Time.deltaTime * speed;
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            moveY += Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveY -= Time.deltaTime * speed;
        }

        this.gameObject.transform.position = new Vector3(moveX, moveY, this.gameObject.transform.position.z);
    }
}
