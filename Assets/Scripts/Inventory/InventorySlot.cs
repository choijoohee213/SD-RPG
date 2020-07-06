using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public int slotNum;
    public InventoryItem inventoryItem;
    public Image icon;
    public Text num;
    
    public void AddItem(InventoryItem newItem) {
        inventoryItem = newItem;
        icon.sprite = inventoryItem.item.icon;
        icon.enabled = true;
        num.enabled = true;
        UpdateNumText();
    }

    public void ClearSlot() {
        inventoryItem = null;
        icon.sprite = null;
        icon.enabled = false;
        num.enabled = false;
    }

    //인벤토리 UI에서 아이템의 개수가 바뀌어 Text를 바꿀 때 호출
    public void UpdateNumText() {
        num.text = inventoryItem.NumPerCell.ToString();
        num.enabled = true;
    }

    public void OnSlotBtn(int slotNum) {
        if(inventoryItem != null)
            Inventory.Instance.inventoryUIScript.ShowItemInform(slotNum);
    }
    
}
