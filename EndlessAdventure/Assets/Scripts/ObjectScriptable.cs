using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScriptable : MonoBehaviour
{
    public string _oName;
    public Sprite _oSprite;
    public int _atk,_hp;

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
