using System;
using UnityEngine;

[Serializable]
public class ItemProbability {
    public string itemName;

    [Range(1, 10)]
    public int DropProbability; //1~10
}