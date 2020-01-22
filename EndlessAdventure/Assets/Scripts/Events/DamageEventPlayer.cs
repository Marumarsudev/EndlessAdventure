using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEventPlayer : Event
{
    public int damage = 0;

    public void SetDamage(int dmg)
    {
        //damage = dmg;
        damage = GetComponent<HealthComponent>().damage;
    }

    public override void CallEvent(BaseObject target)
    {
        try
        {
            GetComponent<BaseObject>().PlayAttackClip();
            if(GetComponent<BaseObject>().isBackStab)
            {
                target.GetComponent<HealthComponent>().TakeDamage(damage * 2, GetComponent<BaseObject>());
                GetComponent<BaseObject>().isBackStab = false;
            }
            else
            {
                target.GetComponent<HealthComponent>().TakeDamage(damage, GetComponent<BaseObject>());
            }
        }
        catch
        {
            Debug.LogError("Target doesn't have a HealthComponent!");
        }
    }
}
