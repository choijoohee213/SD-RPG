using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIScript : MonoBehaviour
{
    Inventory inventory;

    public Transform itemsParent;
    public InventorySlot[] slots;

    //DetailsUI
    private int selectedSlotNum;
    public Text ItemNameText;
    public Image ItemImg;
    public GameObject UseItemBtn;


    void Awake()
    {
        Inventory.Instance.Space = slots.Length;
        inventory = Inventory.Instance;
        Inventory.Instance.Init();
    }

    public void UpdateUI() {
        for(int i = 0; i < slots.Length; i++) {
            if(i < inventory.items.Count)
                slots[i].AddItem(inventory.items[i]);
            else
                slots[i].ClearSlot();

        }
    }


    public void ShowItemInform(int slotNum) {
        selectedSlotNum = slotNum;
        
        Item item = inventory.items[selectedSlotNum].item;        
        ItemNameText.text = item.name;
        ItemImg.sprite = item.icon;
        UseItemBtn.SetActive(item.isConsumable);

        GameManager.Instance.ItemDetailsUI.SetActive(true);
    }

    public void OnUseBtn() {
        slots[selectedSlotNum].inventoryItem.item.Use();
        DeleteFromInventory();
    }
    
    public void OnDiscardBtn() {
        DeleteFromInventory();
    }

    private void DeleteFromInventory() {
        bool deleteFromCell = inventory.Remove(selectedSlotNum);
        GameManager.Instance.ItemDetailsUI.SetActive(!deleteFromCell);
    }
   
}
