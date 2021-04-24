using System;
using UnityEngine;

public class AnimationHelper
{
    public static bool hasParameter(Animator animator, String parameter)
    {
        foreach (var animatorControllerParameter in animator.parameters)
        {
            if (animatorControllerParameter.name.Equals(parameter))
            {
                return true;
            }
        }

        return false;
    }
}