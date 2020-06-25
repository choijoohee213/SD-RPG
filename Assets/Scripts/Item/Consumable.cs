using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Consumable")]
public class Consumable : Item
{
    public override void Use() {
        Inventory.Instance.Remove(this);
    }
}
