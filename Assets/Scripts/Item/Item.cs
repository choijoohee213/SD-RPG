using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Other")]
public class Item : ScriptableObject
{
    public new string name = "New Item";
    public Sprite icon = null;

    public virtual void Use() {
    }
}
