using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "RPG/Quest")]
public class Quest : ScriptableObject
{
    public string title;

    [TextArea(2, 6)]
    public string content;

    public CollectObjective[] collectObjectives;
    public bool isCompleted = false;
}


[Serializable]
public abstract class Objective
{
    public Item item;
    public int amount;
    public int currentAmount { get; set; }
}

[Serializable]
public class CollectObjective : Objective 
{
    public void UpdateItemCount() {
        currentAmount = Inventory.Instance.GetItemCount(item);
        Debug.Log("updateItem!!!!!!!!!!!!");
    }   
}
