using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public const String IDLE = "Idle";
    public const String DIRECTION_H = "DirectionH";
    public const String DIRECTION_V = "DirectionV";
    public const String SHOW_RIGHT = "ShowRight";
    public float movementSpeed = 5;
    public int lastDirectionV = 0;
    public int lastDirectionH = 0;
    public Transform weapon;

    // Update is called once per frame
    private void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Rigidbody2D rigidbody = GetComponentInChildren<Rigidbody2D>();
        if (Mathf.Approximately(vertical, 0f) && Mathf.Approximately(horizontal, 0f))
        {
            rigidbody.velocity = Vector2.zero;
        }
        else
        {
            rigidbody.velocity = new Vector2(horizontal, vertical).normalized * movementSpeed;
        }

        bool idle = Mathf.Approximately(vertical, 0f) && Mathf.Approximately(horizontal, 0f);

        bool right = horizontal > 0;

        int directionH = 0;
        if (horizontal < 0)
        {
            directionH = -1;
        }
        else if (horizontal > 0)
        {
            directionH = 1;
        }

        int directionV = 0;
        if (vertical < 0)
        {
            directionV = -1;
        }
        else if (vertical > 0)
        {
            directionV = 1;
        }

        // 0 Idle
        // 1 Right
        // 2 Top
        // 3 TopRight
        // 4 
        // 5
        // 6
        // 7
        // 8

        if (idle)
        {
            rigidbody.position = new Vector2(
                Mathf.Round(rigidbody.position.x * 32) / 32f,
                Mathf.Round(rigidbody.position.y * 32) / 32f
            );
        }

        foreach (Animator animator in GetComponentsInChildren<Animator>())
        {
            if (idle)
            {
                if (AnimationHelper.hasParameter(animator, IDLE))
                {
                    animator.SetBool(IDLE, true);
                }
            }
            else
            {
                RandomizedSound.Play(transform, RandomizedSound.MOVEMENT);
                if (AnimationHelper.hasParameter(animator, IDLE))
                {
                    animator.SetBool(IDLE, false);
                }

                if (AnimationHelper.hasParameter(animator, DIRECTION_H))
                {
                    animator.SetInteger(DIRECTION_H, directionH);
                }

                if (AnimationHelper.hasParameter(animator, DIRECTION_V))
                {
                    animator.SetInteger(DIRECTION_V, directionV);
                }

                if (AnimationHelper.hasParameter(animator, SHOW_RIGHT))
                {
                    if (right)
                    {
                        animator.SetBool(SHOW_RIGHT, true);
                    }
                    else
                    {
                        animator.SetBool(SHOW_RIGHT, false);
                    }
                }

                lastDirectionV = directionV;
                lastDirectionH = directionH;
            }
        }

        ReAnchorWeapon(idle, idle ? lastDirectionH : directionH, idle ? lastDirectionV : directionV);
    }


    private void ReAnchorWeapon(bool idle, int directionH, int directionV)
    {
        if (weapon)
        {
            if (!idle)
            {
                weapon.SetParent(transform.Find("Hand_" + directionH + "_" + directionV), false);
            }
        }
    }
}