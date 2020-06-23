using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    public new string name = "New Item";
    public Sprite icon = null;
}
