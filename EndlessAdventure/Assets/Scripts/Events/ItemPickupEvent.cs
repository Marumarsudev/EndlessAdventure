using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupEvent : Event
{
    public InventoryItem item;

    public override void CallEvent(BaseObject target = null)
    {
        target.inventory.AddItem(item);
    }
}
