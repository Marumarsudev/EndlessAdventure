using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pixelplacement;
using Pixelplacement.TweenSystem;

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
    public TextMeshProUGUI nameText;
    public Lane lane;

    public TweenBase tween = null;

    public List<Event> EntryEvents = new List<Event>();

    public void CallEvents(BaseObject target)
    {
        EntryEvents.ForEach(e => {
            e.CallEvent(target);
        });
    }

    void Start()
    {
        nameText.text = oName;
    }
}
