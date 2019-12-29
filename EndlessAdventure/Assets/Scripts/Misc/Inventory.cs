﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{

    private Vector3 origpos;

    public float moveAmount;

    private bool isOpen = false;
    private bool canInteract = true;

    public GameObject player;
    private BaseObject playerBase;

    private InventoryItem Slot1, Slot2, Slot3;
    public Image Slot1Sprite, Slot2Sprite, Slot3Sprite;

    void Start()
    {
        playerBase = player.GetComponent<BaseObject>();
        origpos = GetComponent<RectTransform>().localPosition;
    }

    public void AddItem(InventoryItem item)
    {
        if(Slot1 == null)
        {
            Slot1 = item;
            Slot1Sprite.enabled = true;
            Slot1Sprite.sprite = item.image;
        }
        else if(Slot2 == null)
        {
            Slot2 = item;
            Slot2Sprite.enabled = true;
            Slot2Sprite.sprite = item.image;
        }
        else if(Slot3 == null)
        {
            Slot3 = item;
            Slot3Sprite.enabled = true;
            Slot3Sprite.sprite = item.image;
        }
        else
        {
            Debug.Log("Inventory Full!");
        }
    }

    public void UseSlot(int slot)
    {
        switch(slot)
        {
            case 1:
                if(Slot1)
                {
                    Slot1.UseItem(playerBase);
                    Slot1 = null;
                    Slot1Sprite.enabled = false;
                    Slot1Sprite.sprite = null;
                }
            break;

            case 2:
                if(Slot2)
                {
                    Slot2.UseItem(playerBase);
                    Slot2 = null;
                    Slot2Sprite.enabled = false;
                    Slot2Sprite.sprite = null;
                }
            break;

            case 3:
                if(Slot3)
                {
                    Slot3.UseItem(playerBase);
                    Slot3 = null;
                    Slot3Sprite.enabled = false;
                    Slot3Sprite.sprite = null;
                }
            break;

            default:
                Debug.Log("Not an inventory slot nani fuck?");
            break;
        }
    }

    public void OpenCloseInventory()
    {
        
        if(canInteract)
        {
            canInteract = false;
            if(isOpen)
            {
                GetComponent<RectTransform>().DOLocalMove(origpos, 1f).SetEase(Ease.OutCubic).OnComplete(() => {canInteract = true;});
            }
            else
            {
                GetComponent<RectTransform>().DOLocalMove(origpos + new Vector3(0, moveAmount, 0), 1f).SetEase(Ease.OutCubic).OnComplete(() => {canInteract = true;});
            }
            isOpen = !isOpen;
        }
    }

}