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
    int beforeSlotNum;

    private void Awake() {
        questSlots = new List<QuestSlot>();
        beforeSlotNum = -1;
        listCountText.text = 0 + " / " + questSlots.Count;

        for(int i=0; i<10; i++) GameManager.Instance.objectPool.Generate("QuestListSlot", false);
    }

    public void AddQuest(Quest _quest) {
        QuestSlot slot = GameManager.Instance.objectPool.GetObject("QuestListSlot").GetComponent<QuestSlot>();

        slot.quest = _quest;
        slot.Init(questSlots.Count);

        questSlots.Add(slot);
    }

   public void ShowQuestContent(int _slotNum) {
        if(beforeSlotNum != -1)
            questSlots[beforeSlotNum].selected = false;
        
        beforeSlotNum = _slotNum;

        titleText.text = questSlots[_slotNum].quest.title;
        contentText.text = questSlots[_slotNum].quest.content + "\n\n";

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
        
        if(questSlots.Count <= 0 || beforeSlotNum.Equals(-1)) return;

        foreach(var obj in questSlots[beforeSlotNum].quest.collectObjectives) {
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
        if(questSlots.Count > 0)
            listCountText.text = beforeSlotNum + 1 + " / " + questSlots.Count;
    }
}
