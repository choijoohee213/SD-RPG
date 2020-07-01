public class InventoryItem {
    public Item item;
    public int NumPerCell;
    public bool IsFull => NumPerCell.Equals(30);

    public InventoryItem(Item item) {
        this.item = item;
        NumPerCell = 1;
    }
}
