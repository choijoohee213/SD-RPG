using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;

    public Transform itemsParent;
    public InventorySlot[] slots;

    //DetailsUI
    public Item selectedItem;
    public Text ItemNameText;
    public Image ItemImg;
    public GameObject UseItemBtn;


    void Awake()
    {
        Inventory.Instance.Space = slots.Length;
        inventory = Inventory.Instance;
        inventory.onItemChangedCallback += UpdateUI;
        Inventory.Instance.Init();
    }

    void UpdateUI() {
        for(int i=0; i<slots.Length; i++) {
            if(i < inventory.items.Count) 
                slots[i].AddItem(inventory.items[i]);
            
            else 
                slots[i].ClearSlot();
            
        }
    }

    public void ShowItemInform(Item item) {
        selectedItem = item; 
        ItemNameText.text = item.name;
        ItemImg.sprite = item.icon;
        UseItemBtn.SetActive(item.isConsumable);
        Inventory.Instance.ItemDetailsUI.SetActive(true);
    }

    public void OnDiscardBtn() {
        Inventory.Instance.Remove(selectedItem);
        Inventory.Instance.ItemDetailsUI.SetActive(false);
    }
}
