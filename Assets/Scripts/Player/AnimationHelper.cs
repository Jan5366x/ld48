using System;
using UnityEngine;

public class AnimationHelper
{
    public static bool HasParameter(Animator animator, String parameter)
    {
        if (!animator)
        {
            return false;
        }

        foreach (var animatorControllerParameter in animator.parameters)
        {
            if (animatorControllerParameter.name.Equals(parameter))
            {
                return true;
            }
        }

        return false;
    }

    public static void SetParameter(Animator animator, String parameter, bool value)
    {
        if (!animator)
        {
            return;
        }

        if (!HasParameter(animator, parameter)) return;

        animator.SetBool(parameter, value);
    }

    public static void SetParameter(Animator animator, String parameter, float value)
    {
        if (!animator)
        {
            return;
        }

        if (!HasParameter(animator, parameter)) return;

        animator.SetFloat(parameter, value);
    }

    public static void SetParameter(Animator animator, String parameter, int value)
    {
        if (!animator)
        {
            return;
        }

        if (!HasParameter(animator, parameter)) return;

        animator.SetInteger(parameter, value);
    }
}