using System;
using Unity.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public const String IDLE = "Idle";
    public const String DIRECTION_H = "DirectionH";
    public const String DIRECTION_V = "DirectionV";

    [ReadOnly] public float speed;
    public float walkingSpeed = 5;
    public float sprintSpeed = 10;

    [ReadOnly] public bool previousSprint = false;
    [ReadOnly] public int lastDirectionV = 0;
    [ReadOnly] public int lastDirectionH = 0;
    public Transform weapon;

    // Update is called once per frame
    private void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        bool isSprint = Player.CalculateStaminaTick(Input.GetButton("Fire3"));
        previousSprint = isSprint;
        speed = isSprint ? sprintSpeed : walkingSpeed;

        Rigidbody2D rigidbody = GetComponentInChildren<Rigidbody2D>();
        if (Mathf.Approximately(vertical, 0f) && Mathf.Approximately(horizontal, 0f))
        {
            rigidbody.velocity = Vector2.zero;
        }
        else
        {
            rigidbody.velocity = new Vector2(horizontal, vertical).normalized * speed;
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
                AnimationHelper.SetParameter(animator, IDLE, true);
            }
            else
            {
                RandomizedSound.Play(transform, RandomizedSound.MOVEMENT);
                AnimationHelper.SetParameter(animator, IDLE, false);
                AnimationHelper.SetParameter(animator, DIRECTION_H, directionH);
                AnimationHelper.SetParameter(animator, DIRECTION_V, directionV);

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