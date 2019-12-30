using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEffectOnPlayerEvent : Event
{
    public GameObject Effect;
    public RuntimeAnimatorController controller;
    public Vector2 offset;
    public override void CallEvent(BaseObject target = null)
    {
        if(Effect == null)
        {
            target.GetComponent<PlayerEffect>().PlayEffectOnPlayer(controller, offset);
        }
        else
        {
            GameObject temp = Instantiate(Effect, (Vector2)target.transform.position + offset, Quaternion.identity, target.transform);
            Animator tempAnimator = temp.GetComponent<Animator>();
            tempAnimator.runtimeAnimatorController = controller;
            tempAnimator.SetTrigger("Trigger");
            Destroy(tempAnimator.gameObject, 1f);
        }
    }
}
