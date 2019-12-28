using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfEvent : Event
{
    public delegate void DestroyAction();

    public event DestroyAction ContinuePlay;

    public override void CallEvent(BaseObject target)
    {
        ContinuePlay();
        Destroy(gameObject);
    }
}
