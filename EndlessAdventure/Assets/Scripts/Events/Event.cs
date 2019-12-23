using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    public virtual void CallEvent(BaseObject target)
    {
        Debug.Log($"this is the default event sent from {GetComponent<BaseObject>().oName} to {target.oName}.");
    }
}
