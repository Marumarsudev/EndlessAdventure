using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEvent : Event
{
    public override void CallEvent(BaseObject target)
    {
        try
        {
            target.GetComponent<HealthComponent>().TakeDamage(GetComponent<HealthComponent>().curHealth, GetComponent<BaseObject>());
        }
        catch
        {
            Debug.LogError("Target doesn't have a HealthComponent!");
        }
    }
}
