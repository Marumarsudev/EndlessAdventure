using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationEvent : Event
{
    public override void CallEvent(BaseObject target)
    {
        GetComponent<Animator>().SetTrigger("Attack");
    }
}
