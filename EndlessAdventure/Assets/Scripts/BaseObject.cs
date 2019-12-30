using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public enum BaseObjectType
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
    public BaseObjectType oType;
    public string oName;
    //public TextMeshProUGUI nameText;
    public Lane lane;

    public delegate void AnimationAction();
    public event AnimationAction Attack;
    
    public event AnimationAction AttackEnd;
    public event AnimationAction Attacked;

    public List<Event> EntryEvents = new List<Event>();
    public List<Event> EntryAnimationEvents = new List<Event>();

    public Inventory inventory;

    public List<AudioClip> AttackAudio = new List<AudioClip>();
    public List<AudioClip> HurtAudio = new List<AudioClip>();
    public List<AudioClip> DeathAudio = new List<AudioClip>();

    private AudioSource audioSource;

    public int InvisibilityTime = 0;

    private bool isInvisible = false;

    public void ChangeInvisibilityTime(int amount)
    {
        InvisibilityTime += amount;

        if(InvisibilityTime < 0)
            InvisibilityTime = 0;

        if(InvisibilityTime > 0 && !isInvisible)
        {
            MakeInvisible();
        }
        else if(InvisibilityTime <= 0 && isInvisible)
        {
            MakeVisible();
        }
    }

    private void MakeInvisible()
    {
        Debug.Log("BRAVO SIX, GOING DARK!");
        GetComponent<SpriteRenderer>().material.DOFade(0.2f, 0.75f).SetEase(Ease.InFlash);
        isInvisible = true;
    }

    private void MakeVisible()
    {
        GetComponent<SpriteRenderer>().DOFade(1f, 0.75f);
        GetComponent<SpriteRenderer>().material.DOFade(1f, 0.75f).SetEase(Ease.InFlash);
        isInvisible = false;
    }

    public void PlayAttackClip()
    {
        if(AttackAudio.Count > 0)
            audioSource.PlayOneShot(AttackAudio[Random.Range(0, AttackAudio.Count)]);
    }

    public void PlayHurtClip()
    {
        if(HurtAudio.Count > 0)
            audioSource.PlayOneShot(HurtAudio[Random.Range(0, HurtAudio.Count)]);
    }

    public void PlayDeathClip()
    {
        if(DeathAudio.Count > 0)
            audioSource.PlayOneShot(DeathAudio[Random.Range(0, DeathAudio.Count)]);
    }

    public void TriggerAttacked()
    {
        Attacked();
    }

    public void TriggerAttackEnd()
    {
        AttackEnd();
    }

    public void TriggerAttack()
    {
        Attack();
    }

    public void CallEvents(BaseObject target)
    {
        EntryEvents.ForEach(e => {
            e.CallEvent(target);
        });
    }

    public void CallAnimationEvents()
    {
        if(GetComponent<HealthComponent>().curHealth > 0)
            EntryAnimationEvents.ForEach(e => {
                e.CallEvent();
            });
    }

    public void SetLookDirection(int direction)
    {
        if(direction == -1)
            GetComponent<SpriteRenderer>().flipX = true;
        else if(direction == 0)
        {
            float rand = Random.Range(0f,1f);
            if(rand > 0.5)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
