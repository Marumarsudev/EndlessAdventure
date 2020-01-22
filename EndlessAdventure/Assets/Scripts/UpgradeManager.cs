using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using EasyMobile;

public class UpgradeManager : MonoBehaviour
{
    public GameObject BackGround1;
    public GameObject BackGround2;

    private float yOffset = 0.5f;

    SaveData playerSave;
    public Button upgradeAttackBtn;
    public Button upgradeDefenceBtn;
    
    public TextMeshProUGUI upgradeAttackTxt;
    public TextMeshProUGUI upgradeDefenceTxt;
    public TextMeshPro statsTxt;

    public SpriteRenderer fade;

    public int basePrice;

    public float attackPriceMult;
    public float defencePriceMult;

    public int attackUpgrade;
    public int defenceUpgrade;

    private int attackPrice;
    private int defencePrice;

    // Start is called before the first frame update
    void Start()
    {
        fade.color = Color.black;
        fade.DOFade(0, 0.75f).SetEase(Ease.OutCubic);
        if(LocalSaveHandler.LoadData("PlayerSaveData") != null)
        {
            playerSave = LocalSaveHandler.LoadData("PlayerSaveData") as SaveData;
        }
        else
        {
            playerSave = new SaveData(null);
        }
        UpdatePrices();
        MoveBG1();
        MoveBG2();
    }

    private void MoveBG1()
    {
        BackGround1.transform.DOMove(new Vector3(BackGround1.transform.position.x, BackGround1.transform.position.y - yOffset, 0), 0.5f)
        .OnComplete(() => {
            if(BackGround1.transform.position.y <= -11f)
            {
                BackGround1.transform.position = new Vector3(0,BackGround2.transform.position.y + 13.75f,0);
                MoveBG1();
            }
            else
            {
                MoveBG1();
            }
        });
    }

    private void MoveBG2()
    {
        BackGround2.transform.DOMove(new Vector3(BackGround2.transform.position.x, BackGround2.transform.position.y - yOffset, 0), 0.5f)
        .OnComplete(() => {
            if(BackGround2.transform.position.y <= -11f)
            {
                BackGround2.transform.position = new Vector3(0,BackGround1.transform.position.y + 13.75f,0);
                MoveBG2();
            }
            else
            {
                MoveBG2();
            }
        });
    }

    public void ChangeScene(string scene)
    {
        fade.DOFade(1, 0.75f).SetEase(Ease.OutCubic).OnComplete(() => {SceneManager.LoadScene(scene);});
    }

    public void UpgradeAttack()
    {
        if(playerSave.gold >= attackPrice)
        {
            playerSave.gold -= attackPrice;
            playerSave.attackUpgrades++;
            playerSave.playerAttack += attackUpgrade;
            UpdatePrices();
            SaveData();
        }
    }

    public void UpgradeDefence()
    {
        if(playerSave.gold >= defencePrice)
        {
            playerSave.gold -= defencePrice;
            playerSave.defenceUpgrades++;
            playerSave.playerStartDefence += defenceUpgrade;
            playerSave.playerMaxDefence += defenceUpgrade;
            UpdatePrices();
            SaveData();
        }
    }

    private void UpdatePrices()
    {
        statsTxt.text = $"Attack: {playerSave.playerAttack}\nMax Health: {playerSave.playerMaxDefence}\nGold: {playerSave.gold}";

        attackPrice = Mathf.RoundToInt(basePrice * Mathf.Pow(attackPriceMult, playerSave.attackUpgrades));
        defencePrice = Mathf.RoundToInt(basePrice * Mathf.Pow(defencePriceMult, playerSave.defenceUpgrades));

        upgradeAttackTxt.text = $"Attack + {attackUpgrade}\n{attackPrice} Gold";
        upgradeDefenceTxt.text = $"Health + {defenceUpgrade}\n{defencePrice} Gold";

        if(playerSave.gold < defencePrice)
        {
            upgradeDefenceBtn.interactable = false;
        }
        else
        {
            upgradeDefenceBtn.interactable = true;
        }

        if(playerSave.gold < attackPrice)
        {
            upgradeAttackBtn.interactable = false;
        }
        else
        {
            upgradeAttackBtn.interactable = true;
        }
    }

    private void SaveData()
    {
        LocalSaveHandler.SaveData(playerSave, "PlayerSaveData");
    }
}
