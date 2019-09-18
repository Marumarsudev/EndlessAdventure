using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Object : MonoBehaviour
{
    public ObjectScriptable scriptable;

    public TextMeshPro text;

    public SpriteRenderer sprite;
    public SpriteRenderer border;


    private bool dead = false;

    void Start()
    {
        if(scriptable)
            scriptable.Setup();

        if(scriptable.objType == Type.player)
        {
            sprite.sortingOrder = 2;
        }
        else if (scriptable.objType == Type.enemy)
        {
            sprite.sortingOrder = 0;
            border.color = Color.red;
        }
        else if (scriptable.objType == Type.pot)
        {
            sprite.sortingOrder = 0;
            border.color = Color.green;
        }

        text.sortingOrder = sprite.sortingOrder + 1;
        border.sortingOrder = sprite.sortingOrder + 2;

        UpdateText();
    }

    private void UpdateText()
    {
        if(scriptable.objType == Type.player)
        {
            text.text = /*scriptable.oName + "\n"+*/scriptable.hp.ToString()/*+"/"+scriptable.maxHp.ToString()*/;
        }
        else if(scriptable.objType == Type.enemy)
        {
            text.text = /*scriptable.oName + "\n"*/scriptable.hp.ToString();
        }
        else if (scriptable.objType == Type.pot)
        {
            text.text = /*scriptable.oName + "\n*/"+"+scriptable.atk.ToString();
        }
        else if (scriptable.objType == Type.pois)
        {
            text.text = /*scriptable.oName + "\n*/"-"+scriptable.atk.ToString();
        }
    }

    public void ChangeSprite()
    {
        sprite.sprite = scriptable.oSprite;
    }

    public void Damage(int amount)
    {
        amount = Mathf.Abs(amount);
        scriptable.hp -= amount;
        if(scriptable.hp <= 0)
        {
            dead = true;
            Debug.Log(scriptable.oName + " died.");
            DestroyImmediate(this.gameObject);
        }
        UpdateText();
    }

    public void Heal(int amount)
    {
        Debug.Log(amount);
        scriptable.hp += amount;
        if(scriptable.hp > scriptable.maxHp)
            scriptable.hp = scriptable.maxHp;
        UpdateText();
    }

    public void HealStr(int amount)
    {
        scriptable.atk += amount;
        UpdateText();
    }

    public void DamageStr(int amount)
    {
        amount = Mathf.Abs(amount);
        scriptable.atk -= amount;
        if(scriptable.atk < 0)
            scriptable.atk = 0;
        UpdateText();
    }

    public void CallEntryEvents(Object target, Object self)
    {
        scriptable.OnEntryEvents.ForEach(e => {
            e.CallEvent(target, self);
        });
    }

    public void CallExitEvents(Object target, Object self)
    {
        scriptable.OnExitEvents.ForEach(e => {
            e.CallEvent(target, self);
        });
    }
}