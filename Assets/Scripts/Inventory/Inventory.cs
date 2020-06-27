using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    public GameObject InventoryUI, ItemDetailsUI;

    public int Space { get; set; }
    public bool NotEnoughRoom => items.Count >= Space;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<Item> items = new List<Item>();

    private void Update() {
        if(Input.GetButtonDown("Jump")) {
            Remove(items[0]);
            print("removed!!");
        }
        if(Input.GetKeyDown(KeyCode.I)) {
            InventoryUI.gameObject.SetActive(!InventoryUI.gameObject.activeSelf);
            ItemDetailsUI.SetActive(false);
        }
    }

    public void Init() {
        onItemChangedCallback?.Invoke();
    }

    public bool Add (Item item) {
        if(NotEnoughRoom) {
            return false;
        }
        items.Add(item);
        
        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        
        return true;
    }

    public void Remove(Item item) {
        items.Remove(item);

        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
