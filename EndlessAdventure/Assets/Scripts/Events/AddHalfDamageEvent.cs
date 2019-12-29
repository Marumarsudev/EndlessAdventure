using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHalfDamageEvent : Event
{
    public int HalfDamageCount;
    public override void CallEvent(BaseObject target = null)
    {
        target.GetComponent<HealthComponent>().AddHalfDamageCount(HalfDamageCount);
    }
}
