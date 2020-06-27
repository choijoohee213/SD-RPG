using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public new string name = "New Item";
    public Sprite icon = null;
    public bool isConsumable;

    public void Use() {
    }
}
