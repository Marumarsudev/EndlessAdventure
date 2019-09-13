using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    player,
    enemy,
    pot,
    pois
}

public class ObjectScriptable : MonoBehaviour
{
    public string _oName;
    public Sprite _oSprite;
    public int _atk,_hp;

    public Type objType;

    public int maxHp;

    [System.NonSerialized] public string oName;
    [System.NonSerialized] public Sprite oSprite;
    [System.NonSerialized] public int atk,hp;

    public List<Event> OnEntryEvents = new List<Event>();
    public List<Event> OnExitEvents = new List<Event>();

    public void Setup()
    {
        oName = _oName;
        oSprite = _oSprite;
        atk = _atk;
        hp = _hp;
    }
}
