using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIScript : Singleton<QuestUIScript>
{
    List<QuestSlot> slots;

    public Text titleText, contentText, listCountText;
    int beforeSlotNum;

    private void Awake() {
        slots = new List<QuestSlot>();
        beforeSlotNum = 9999;
        listCountText.text = 0 + " / " + slots.Count;

        for(int i=0; i<10; i++) GameManager.Instance.objectPool.Generate("QuestListSlot", false);
    }

    public void AddQuest(Quest _quest) {
        QuestSlot questSlot = GameManager.Instance.objectPool.GetObject("QuestListSlot").GetComponent<QuestSlot>();

        questSlot.quest = _quest;
        questSlot.Init(slots.Count);
        
        slots.Add(questSlot);
    }

   public void ShowQuestContent(int _slotNum) {
        if(beforeSlotNum != 9999) 
            slots[beforeSlotNum].selected = false;
        
        beforeSlotNum = _slotNum;

        titleText.text = slots[_slotNum].quest.title;
        contentText.text = slots[_slotNum].quest.content;
        ChangeCountText();
    }

    public void ChangeCountText() {
        listCountText.text = beforeSlotNum+1 + " / " + slots.Count;
    }
}
