using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthEvent : Event
{
    public override void CallEvent(Object target, Object self)
    {
        target.HealStr(self.scriptable.atk);
    }
}
