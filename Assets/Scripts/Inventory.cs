using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    [SerializeField]
    private int space = 20;

    public bool NotEnoughRoom => items.Count >= space;

    public List<Item> items = new List<Item>();

    private void Update() {
        if(Input.GetButtonDown("Jump")) {
            Remove(items[0]);
            print("removed!!");
        }    
    }

    public bool Add (Item item) {
        if(NotEnoughRoom) {
            Debug.Log("Not enoubh room");
            return false;
        }
        items.Add(item);
        
        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        
        return true;
    }

    public void Remove(Item item) {
        items.Remove(item);

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Item"), false);

        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
