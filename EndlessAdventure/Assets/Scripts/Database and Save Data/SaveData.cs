using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int highscore;
    public int gold;
    public int playerAttack;
    public int playerStartDefence;
    public int playerMaxDefence;

    public int attackUpgrades;
    public int defenceUpgrades;

    public SaveData(SaveData save)
    {
        if(save != null)
        {
            highscore = save.highscore;
            gold = save.gold;
            playerAttack = save.playerAttack;
            playerStartDefence = save.playerStartDefence;
            playerMaxDefence = save.playerMaxDefence;
            attackUpgrades = save.attackUpgrades;
            defenceUpgrades = save.defenceUpgrades;
        }
        else
        {
            highscore = 0;
            gold = 0;
            playerAttack = 5;
            playerStartDefence = 10;
            playerMaxDefence = 20;
            attackUpgrades = 0;
            defenceUpgrades = 0;
        }

    }

}
