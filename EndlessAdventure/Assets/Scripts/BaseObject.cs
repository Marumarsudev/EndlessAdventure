using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Type
{
    player,
    enemy,
    item
}

public enum Lane
{
    left = -1,
    middle,
    right
}

public class BaseObject : MonoBehaviour
{
    public Type oType;
    public string oName;
    //public TextMeshProUGUI nameText;
    public Lane lane;

    public delegate void AnimationAction();
    public event AnimationAction Attack;
    
    public event AnimationAction AttackEnd;
    public event AnimationAction Attacked;

    public List<Event> EntryEvents = new List<Event>();
    public List<Event> EntryAnimationEvents = new List<Event>();

    public Inventory inventory;

    public void TriggerAttacked()
    {
        Attacked();
    }

    public void TriggerAttackEnd()
    {
        AttackEnd();
    }

    public void TriggerAttack()
    {
        Attack();
    }

    public void CallEvents(BaseObject target)
    {
        EntryEvents.ForEach(e => {
            e.CallEvent(target);
        });
    }

    public void CallAnimationEvents()
    {
        if(GetComponent<HealthComponent>().curHealth > 0)
            EntryAnimationEvents.ForEach(e => {
                e.CallEvent();
            });
    }

    public void SetLookDirection(int direction)
    {
        if(direction == -1)
            GetComponent<SpriteRenderer>().flipX = true;
        else if(direction == 0)
        {
            float rand = Random.Range(0f,1f);
            if(rand > 0.5)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

    }

    void Start()
    {
        //nameText.text = oName;
    }
}
