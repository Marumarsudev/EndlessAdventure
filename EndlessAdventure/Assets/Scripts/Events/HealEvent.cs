using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealEvent : Event
{
    public int healAmount;

    public TextMeshProUGUI statusText;

    private void Start()
    {
        statusText.text = healAmount.ToString();
    }

    public override void CallEvent(BaseObject target)
    {
        try
        {
            target.GetComponent<HealthComponent>().TakeHealth(healAmount, GetComponent<BaseObject>());
        }
        catch
        {
            Debug.LogError("Target doesn't have a HealthComponent!");
        }
    }
}
