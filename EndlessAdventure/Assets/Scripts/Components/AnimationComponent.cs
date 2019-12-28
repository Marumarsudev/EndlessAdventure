using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    public Animator animator;

    public void TriggerAnimation(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    public void SetBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }
}
