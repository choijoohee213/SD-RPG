using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : Singleton<Inventory>
{
    public InventoryUIScript inventoryUIScript;

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
}


