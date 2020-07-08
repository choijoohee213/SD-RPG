using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "RPG/Item/Other")]
public class Item : ScriptableObject {
    public new string name = "New Item";
    public Sprite icon = null;
    public bool isConsumable;

    public virtual void Use() {
    }
}