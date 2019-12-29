using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    public Animator animator;
    public Transform offset;

    public void PlayEffectOnPlayer(RuntimeAnimatorController controller, Vector2 off)
    {
        animator.runtimeAnimatorController = controller;
        offset.localPosition = off;
        animator.SetTrigger("Trigger");
    }
}
