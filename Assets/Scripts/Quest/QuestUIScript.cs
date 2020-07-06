using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIScript : Singleton<QuestUIScript>
{
    public List<QuestSlot> questSlots;

    public Text titleText, contentText, listCountText;
    public GameObject[] objectives;
    QuestSlot beforeSlot;

    private void Awake() {
        questSlots = new List<QuestSlot>();
        listCountText.text = 0 + " / " + questSlots.Count;

        for(int i=0; i<3; i++) GameManager.Instance.objectPool.Generate("QuestListSlot", false);
    }

    public void AddQuest(Quest _quest) {
        QuestSlot slot = GameManager.Instance.objectPool.GetObject("QuestListSlot").GetComponent<QuestSlot>();

        slot.quest = _quest;
        slot.Init();

        questSlots.Add(slot);
        SetSlotNums();
    }

    public void RemoveQuest(Quest _quest) {
        QuestSlot slotToRemove = questSlots[0];
        foreach(var slot in questSlots) {
            if(slot.quest.Equals(_quest)) {
                slotToRemove = slot;
                break;
            }
        }
        slotToRemove.Removed();
        questSlots.Remove(slotToRemove);
        if(beforeSlot.Equals(slotToRemove)) {
            beforeSlot = null;
            titleText.text = "";
            contentText.text = "";
        }
        SetSlotNums();
    }

    public void ShowQuestContent(QuestSlot slot) {
        if(beforeSlot != null) 
            beforeSlot.Selected = false;
        
        
        beforeSlot = slot;

        titleText.text = slot.quest.title;
        contentText.text = slot.quest.content + "\n\n";

        SetObjectivesUI();
        SetListCountText();
    }

    public void UpdateAllObjectives() {
        foreach(var slot in questSlots) {
            foreach(var obj in slot.quest.collectObjectives) 
                obj.UpdateItemCount();
            slot.CheckCompletable();
        }
    }

    public void SetObjectivesUI() {
        int objectiveCount = 0;
        for(int i = 0; i < objectives.Length; i++)
            objectives[i].SetActive(false);
        
        if(beforeSlot == null) return;

        foreach(var obj in beforeSlot.quest.collectObjectives) {
            objectives[objectiveCount].SetActive(true);

            Image objectiveItem = objectives[objectiveCount].GetComponent<Image>();
            objectiveItem.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            objectiveItem.sprite = obj.item.icon;
           
            Text objectiveText = objectiveItem.transform.GetChild(0).GetComponent<Text>();
            objectiveText.text = obj.item.name + "\n" + obj.currentAmount + "/" + obj.amount;

            objectiveCount++;
        }
    }
    

    public void SetListCountText() {
        if(beforeSlot !=  null)
            listCountText.text = beforeSlot.slotNum + 1 + " / " + questSlots.Count;
        else
            listCountText.text = "0 / " + questSlots.Count;
    }

    private void SetSlotNums() {
        for(int i=0; i<questSlots.Count; i++) {
            questSlots[i].slotNum = i;
        }
    }
}
