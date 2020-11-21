using UnityEngine;
using UnityEngine.UI;

public class InventoryUIScript : MonoBehaviour {
    private Inventory inventory;

    public Transform itemsParent;
    public InventorySlot[] slots;

    //DetailsUI
    public int selectedSlotNum { get; set; }

    public int discardNum { get; set; }

    public Text ItemNameText;
    public Image ItemImg;
    public GameObject UseItemBtn, DiscardUI;
    public InputField discardNumIF;

    private void Awake() {
        Inventory.Instance.Space = slots.Length;
        inventory = Inventory.Instance;
        UpdateUI();
    }

    public void UpdateUI() {
        for(int i = 0; i < slots.Length; i++) {
            if(i < inventory.items.Count)
                slots[i].AddItem(inventory.items[i]);
            else
                slots[i].ClearSlot();
        }
        QuestUIScript.Instance.UpdateAllObjectives();
    }

    public void ShowItemInform(int slotNum) {
        selectedSlotNum = slotNum;

        Item item = inventory.items[selectedSlotNum].item;
        ItemNameText.text = item.name;
        ItemImg.sprite = item.icon;
        UseItemBtn.SetActive(item.isConsumable);

        UIManager.Instance.ItemDetailsUI.SetActive(true);
    }

    public void OnUseBtn() {
        slots[selectedSlotNum].inventoryItem.item.Use();
        DeleteFromInventory();
    }

    public void OnDiscardBtn() {
        DiscardUI.SetActive(!DiscardUI.activeSelf);
        if(DiscardUI.activeSelf)
            discardNumIF.text = 1.ToString();
    }

    public void OnDiscardOKBtn() {
        discardNum = int.Parse(discardNumIF.text);
        DeleteFromInventory();
    }

    public void LimitInputRange() {
        int inputNum = int.Parse(discardNumIF.text);
        if(inputNum < 1) {
            discardNumIF.text = 1.ToString();
        }
        if(inputNum > inventory.items[selectedSlotNum].NumPerCell) {
            discardNumIF.text = inventory.items[selectedSlotNum].NumPerCell.ToString();
        }
    }

    private void DeleteFromInventory() {
        bool deleteFromCell = inventory.Remove(1);
        UIManager.Instance.ItemDetailsUI.SetActive(!deleteFromCell);
    }
}