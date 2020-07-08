public class InventoryItem {
    public Item item;
    public int NumPerCell;
    public bool IsFull => Inventory.Instance.MaxNumPerCell.Equals(NumPerCell);

    public InventoryItem(Item item) {
        this.item = item;
        NumPerCell = 1;
    }
}