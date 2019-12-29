using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEvent : Event
{
    public override void CallEvent(BaseObject target)
    {
            this.GetComponent<BaseObject>().PlayAttackClip();
            target.GetComponent<HealthComponent>().TakeDamage(GetComponent<HealthComponent>().curHealth, GetComponent<BaseObject>());
    }
}
