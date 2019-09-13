using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEvent : Event
{
    public override void CallEvent(Object target, Object self)
    {
        target.Damage(self.scriptable.atk);
    }
}
