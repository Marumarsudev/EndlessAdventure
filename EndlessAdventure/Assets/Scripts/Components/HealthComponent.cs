using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthComponent : MonoBehaviour
{
    public int maxHealth;
    [SerializeField]
    private int startingHealth;
    public int curHealth;

    public List<Event> deathEvents = new List<Event>();

    public List<Event> effectEvents = new List<Event>();

    public float effectRate;
    private float effectTimer = 0f;

    public TextMeshPro statusText;

    [SerializeField]
    public bool destroyAfterDeath = true;

    public int halfDamageCount = 0;

    void Start()
    {
        curHealth = startingHealth;
        UpdateUI();
    }

    void Update()
    {
        effectTimer += Time.deltaTime;
        if(effectTimer >= effectRate)
        {
            if(halfDamageCount > 0)
            {
                effectEvents.ForEach(e => {
                    e.CallEvent(GetComponent<BaseObject>());
                });
            }
            effectTimer = 0f;
        }
    }

    public void SendDamage(HealthComponent target)
    {
        target.TakeDamage(curHealth, GetComponent<BaseObject>());
    }

    public void SendHealth(HealthComponent target)
    {
        target.TakeHealth(curHealth, GetComponent<BaseObject>());
    }

    public void TakeDamage(int dmg, BaseObject attacker)
    {
        GetComponent<BaseObject>().PlayHurtClip();
        if(halfDamageCount <= 0)
            curHealth -= dmg;
        else
        {
            halfDamageCount--;
            if(halfDamageCount < 0)
                halfDamageCount = 0;
            curHealth -= dmg/2;
        }

        if (curHealth <= 0)
        {
            Death(attacker);
        }

        UpdateUI();
    }

    public void AddHalfDamageCount(int amount)
    {
        halfDamageCount += amount;
    }

    public void TakeMaxHealth(int amount)
    {
        maxHealth += amount;
    }

    public void TakeHealth(int heal, BaseObject attacker, bool setHealth = false)
    {
        if(!setHealth)
            curHealth += heal;
        else
            curHealth = heal;

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if(curHealth > 0)
            statusText.text = $"{curHealth.ToString()}";
        else
            statusText.text = "";
    }

    private void Death(BaseObject attacker)
    {
        deathEvents.ForEach(e => {
            e.CallEvent(attacker);
        });

        GetComponent<BaseObject>().PlayDeathClip();

        GetComponent<Animator>().SetBool("Dead", true);
    }

    public void DestroySelf()
    {
        if(destroyAfterDeath)
            Destroy(this.gameObject);
    }

}
