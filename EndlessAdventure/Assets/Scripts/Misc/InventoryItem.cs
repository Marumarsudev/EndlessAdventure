using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public List<Event> events = new List<Event>();
    public Sprite image;
    public string itemname;

    public void UseItem(BaseObject target)
    {
        events.ForEach(e => {
            e.CallEvent(target);
        });
    }
}
