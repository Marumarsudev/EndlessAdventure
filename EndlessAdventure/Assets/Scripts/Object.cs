using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Object : MonoBehaviour
{
    public ObjectScriptable scriptable;

    public TextMeshProUGUI text;

    void Start()
    {
        text.text = scriptable.oName + "\n"+scriptable.atk.ToString()+"/"+scriptable.hp.ToString();
    }

    public void CallEntryEvents()
    {
        scriptable.OnEntryEvents.ForEach(e => {
            e.CallEvent();
        });
    }

    public void CallExitEvents()
    {
        scriptable.OnExitEvents.ForEach(e => {
            e.CallEvent();
        });
    }
}