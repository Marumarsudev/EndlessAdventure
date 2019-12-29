using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEffectOnPlayerEvent : Event
{
    public RuntimeAnimatorController controller;
    public Vector2 offset;
    public override void CallEvent(BaseObject target = null)
    {
        target.GetComponent<PlayerEffect>().PlayEffectOnPlayer(controller, offset);
    }
}
