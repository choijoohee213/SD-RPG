using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestState {
    Startable, 
    Progressing,
    Completable,
    Complete
}

[CreateAssetMenu(fileName = "New Quest", menuName = "RPG/Quest")]
public class Quest : ScriptableObject
{
    public QuestState state = QuestState.Startable;

    public string title;

    [TextArea(2, 6)]
    public string content;

    public CollectObjective[] collectObjectives;

    public bool IsCompleteObjectives {
        get {
            foreach(var o in collectObjectives) {
                if(!o.IsComplete)
                    return false;
            }
            return true;
        }
    }
}


[Serializable]
public abstract class Objective
{
    public Item item;
    public int amount;
    public int currentAmount { get; set; }

    public bool IsComplete { get { return currentAmount >= amount; } }
}

[Serializable]
public class CollectObjective : Objective 
{
    public void UpdateItemCount() {
        currentAmount = Inventory.Instance.GetItemCount(item);
        Debug.Log("updateItem!!!!!!!!!!!!");
    }   
}
