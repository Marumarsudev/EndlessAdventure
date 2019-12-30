using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPickupEvent : Event
{
    public InventoryItem item;

    public TextMeshPro desc;

    void Start()
    {
        desc.text = item.desc;
    }

    public override void CallEvent(BaseObject target = null)
    {
        target.inventory.AddItem(item);
    }
}
