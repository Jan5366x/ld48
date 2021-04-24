using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    public bool isFollowingX;
    public bool isFollowingY;
    public float speed;
    public float speedX;
    public float speedY;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player)
        {
            Vector3 delta = player.transform.position - transform.position;
            
            if (Mathf.Abs(delta.x) > 5)
            {
                isFollowingX = true;
            } else if (Mathf.Abs(delta.x) < 1)
            {
                isFollowingX = false;
            }
            
            if (Mathf.Abs(delta.y) > 2)
            {
                isFollowingY = true;
            } else if (Mathf.Abs(delta.y) < 0.5)
            {
                isFollowingY = false;
            }

            if (isFollowingX || isFollowingY)
            {
                speedX = isFollowingX ? delta.x : 0;
                speedY = isFollowingY ? delta.y : 0;
                Vector2 dir = new Vector2(speedX, speedY);
                dir = dir.normalized * (speed * Time.deltaTime);

                transform.Translate(dir.x, dir.y, 0);
            }
        }
    }
}