using System;
using UnityEngine;

[Serializable]
public class ItemProbability {
    public string ItemName;

    [Range(1, 10)]
    public int DropProbability; //1~10
}