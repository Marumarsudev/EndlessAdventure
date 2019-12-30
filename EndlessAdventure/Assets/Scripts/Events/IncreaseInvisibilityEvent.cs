using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseInvisibilityEvent : Event
{
    public int amount;

    public override void CallEvent(BaseObject target = null)
    {
        target.ChangeInvisibilityTime(amount);
    }
}
