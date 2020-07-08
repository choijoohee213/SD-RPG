using System.Collections.Generic;

public class Inventory : Singleton<Inventory> {
    public InventoryUIScript inventoryUIScript;

    public int MaxNumPerCell;
    public int Space { get; set; }
    public bool NotEnoughRoom => items.Count >= Space;

    public List<InventoryItem> items = new List<InventoryItem>();

    public void Init() {
        //inventoryUIScript.UpdateUI();
    }

    public bool Add(Item _item) {
        for(int i = 0; i < items.Count; i++) {
            if(items[i].item.Equals(_item) && !items[i].IsFull) {
                items[i].NumPerCell++;
                inventoryUIScript.slots[i].UpdateNumText();
                return true;
            }
        }

        if(NotEnoughRoom)
            return false;
        else
            items.Add(new InventoryItem(_item));

        inventoryUIScript.UpdateUI();

        return true;
    }

    public bool Remove() {
        int slotNum = inventoryUIScript.selectedSlotNum;
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

    public bool AddMultiple(Item addItem, int count) {
        bool isNewItem = true;
        if(items.Count == 0) {
            InventoryItem newItem = new InventoryItem(addItem);
            items.Add(newItem);
            newItem.NumPerCell = 0;
        }

        for(int i = 0; i < items.Count; i++) {
            if(!items[i].item.Equals(addItem) || items[i].IsFull) {
                if(i == items.Count - 1 && isNewItem) {
                    InventoryItem newItem = new InventoryItem(addItem);
                    items.Add(newItem);
                    newItem.NumPerCell = 0;
                    isNewItem = false;
                }
                continue;
            }

            isNewItem = false;
            if(items[i].NumPerCell + count > MaxNumPerCell) {
                count += items[i].NumPerCell - MaxNumPerCell - 1;
                if(CheckAddable(count)) {
                    items[i].NumPerCell = MaxNumPerCell;
                    items.Add(new InventoryItem(addItem));
                    continue;
                }
                else {
                    inventoryUIScript.NotifyInventoryFull();
                    return false;
                }
            }
            else {
                items[i].NumPerCell += count;
                break;
            }
        }
        inventoryUIScript.UpdateUI();

        return true;
    }

    private bool CheckAddable(int remainNum) {
        int quotient = (remainNum + 1) / MaxNumPerCell;
        int remainder = (remainNum + 1) % MaxNumPerCell;
        if(remainder != 0)
            quotient++;

        print(Space - items.Count);
        print(quotient);
        return Space - items.Count >= quotient;
    }

    public void RemoveMultiple(Item removeItem, int count) {
        for(int i = items.Count - 1; i >= 0; i--) {
            if(!items[i].item.Equals(removeItem))
                continue;

            if(items[i].NumPerCell - count > 0) {
                items[i].NumPerCell -= count;
                break;
            }
            else if(items[i].NumPerCell - count == 0) {
                items.Remove(items[i]);
                break;
            }
            else {
                count = -(items[i].NumPerCell - count);
                items.Remove(items[i]);
            }
        }

        inventoryUIScript.UpdateUI();
    }

    public int GetItemCount(Item _item) {
        int itemCount = 0;
        foreach(var inventoryItem in items) {
            if(inventoryItem != null && inventoryItem.item.name.Equals(_item.name)) {
                itemCount += inventoryItem.NumPerCell;
            }
        }
        return itemCount;
    }
}