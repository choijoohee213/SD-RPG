using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public InventoryItem inventoryItem;
    public Image icon;
    public Text num;
    
    public void AddItem(InventoryItem newItem) {
        inventoryItem = newItem;
        icon.sprite = inventoryItem.item.icon;
        icon.enabled = true;
        UpdateNumText();
    }

    public void ClearSlot() {
        inventoryItem = null;
        icon.sprite = null;
        icon.enabled = false;
        num.enabled = false;
    }

    public void UpdateNumText() {
        num.text = inventoryItem.NumPerCell.ToString();
        num.enabled = true;
    }

    public void OnSlotBtn(int slotNum) {
        if(inventoryItem != null)
            Inventory.Instance.inventoryUIScript.ShowItemInform(slotNum);
    }
    
}
