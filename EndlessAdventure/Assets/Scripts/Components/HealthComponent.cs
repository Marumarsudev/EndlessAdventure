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

    public TextMeshProUGUI statusText;

    void Start()
    {
        curHealth = startingHealth;
        UpdateUI();
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
        curHealth -= dmg;

        if (curHealth <= 0)
        {
            Death(attacker);
        }
        else
            UpdateUI();
    }

    public void TakeHealth(int heal, BaseObject attacker)
    {
        curHealth += heal;

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        statusText.text = $"{curHealth.ToString()}";
    }

    private void Death(BaseObject attacker)
    {
        deathEvents.ForEach(e => {
            e.CallEvent(attacker);
        });

        Destroy(this.gameObject);
    }

}
