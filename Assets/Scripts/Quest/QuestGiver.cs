using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : Interactable
{
    public Quest[] QuestList;

    protected override void Awake() {
        base.Awake();
        radius = 2.3f;
    }

    public override void Interact() {
        foreach(var quest in QuestList) {
            QuestUIScript.Instance.AddQuest(quest);
        }
        hasInteracted = true;
    }
}
