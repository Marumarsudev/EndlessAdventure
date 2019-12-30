using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxHpEvent : Event
{
    public int amount;

    public override void CallEvent(BaseObject target = null)
    {
        try
        {
            target.GetComponent<HealthComponent>().TakeMaxHealth(amount);
            GameObject.FindObjectOfType<GameManager>().UpdateUI();
        }
        catch
        {
            Debug.LogError("Target doesn't have a HealthComponent!");
        }
    }
}
