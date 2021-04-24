using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public const String IDLE = "Idle";
    public const String DIRECTION = "Direction";
    public const String SHOW_RIGHT = "ShowRight";
    public float movementSpeed = 5;
    private bool forceIdleWeapon;
    public int lastDirection = 0;

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

        int directionComponent = 0;
        if (horizontal < 0)
        {
            directionComponent -= 1;
        }
        else if (horizontal > 0)
        {
            directionComponent += 1;
        }

        if (vertical < 0)
        {
            directionComponent -= 2;
        }
        else if (horizontal > 0)
        {
            directionComponent += 2;
        }

        directionComponent += 3;

        if (idle)
        {
            rigidbody.position = new Vector2(Mathf.Round(rigidbody.position.x), Mathf.Round(rigidbody.position.y));
        }
        Debug.Log(rigidbody.position);

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

                if (AnimationHelper.hasParameter(animator, DIRECTION))
                {
                    animator.SetInteger(DIRECTION, directionComponent);
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

                lastDirection = directionComponent;
            }
        }

        AbstractWeapon weapon = GetComponentInChildren<AbstractWeapon>();
        HandleWeapon(weapon, idle, idle ? lastDirection : directionComponent);
    }


    private void HandleWeapon(AbstractWeapon weapon, bool idle, int direction)
    {
        if (weapon)
        {
            if (!idle || forceIdleWeapon)
            {
                switch (direction)
                {
                    case 0:
                        weapon.transform.SetParent(transform.Find("HandForwards"), false);
                        break;
                    case 1:
                        weapon.transform.SetParent(transform.Find("HandLeft"), false);
                        break;
                    case 2:
                        weapon.transform.SetParent(transform.Find("HandBackwards"), false);
                        break;
                    case 3:
                        weapon.transform.SetParent(transform.Find("HandRight"), false);
                        break;
                }
            }

            forceIdleWeapon = false;
        }
    }

    public void OnSwitchWeapon()
    {
        forceIdleWeapon = true;
    }
}