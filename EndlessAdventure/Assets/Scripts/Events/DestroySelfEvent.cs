using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfEvent : Event
{
    public override void CallEvent(BaseObject target)
    {
        Destroy(gameObject);
    }
}
