using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    public virtual void CallEvent(Object target, Object self)
    {
        Debug.Log("Event target: " + target.scriptable.oName);
    }
}
