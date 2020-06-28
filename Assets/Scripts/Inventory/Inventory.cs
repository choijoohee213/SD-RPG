﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem {
    public Item item;
    public int NumPerCell;
    public bool IsFull => NumPerCell.Equals(30);

    public InventoryItem(Item item) {
        this.item = item;
        NumPerCell = 1;
    }
}

public class Inventory : Singleton<Inventory>
{
    public GameObject InventoryUI, ItemDetailsUI;
    public InventoryUI inventoryUIScript;

    public int Space { get; set; }
    public bool NotEnoughRoom => items.Count >= Space;

    public List<InventoryItem> items = new List<InventoryItem>();


    public void Init() {
        inventoryUIScript.UpdateUI();
    }

    public bool Add (Item _item) {
        
        for(int i =0; i<items.Count; i++) {
            if(items[i].item.Equals(_item) && !items[i].IsFull) {
                items[i].NumPerCell++;
                inventoryUIScript.slots[i].UpdateNumText();
                return true;
            }
        }

        if(NotEnoughRoom) return false;
        else items.Add(new InventoryItem(_item));

        inventoryUIScript.UpdateUI();

        return true;
    }

    public bool Remove(int slotNum) {
        if(!items[slotNum].NumPerCell.Equals(1)) {
            items[slotNum].NumPerCell--;
            inventoryUIScript.slots[slotNum].UpdateNumText();
            return false;
        }
        else {
            items.Remove(items[slotNum]);
            inventoryUIScript.UpdateUI();
            return true;
        }

    }

    public void OnInventoryBtn() {
        InventoryUI.gameObject.SetActive(!InventoryUI.gameObject.activeSelf);
        ItemDetailsUI.SetActive(false);
    }
}