using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEventPlayer : Event
{
    public int damage = 0;

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    public override void CallEvent(BaseObject target)
    {
        try
        {
            target.GetComponent<HealthComponent>().TakeDamage(damage, GetComponent<BaseObject>());
        }
        catch
        {
            Debug.LogError("Target doesn't have a HealthComponent!");
        }
    }
}
