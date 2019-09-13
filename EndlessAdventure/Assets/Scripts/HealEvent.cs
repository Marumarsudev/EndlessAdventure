using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEvent : Event
{
    public override void CallEvent(Object target, Object self)
    {
        target.Heal(self.scriptable.atk);
    }
}
