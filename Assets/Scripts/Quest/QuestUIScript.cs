using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIScript : Singleton<QuestUIScript> {
    public List<QuestSlot> questSlots;

    public Text titleText, contentText, listCountText, npcNameText;
    public Image npcImg;
    public GameObject[] objectives;
    private QuestSlot beforeSlot;

    private void Awake() {
        questSlots = new List<QuestSlot>();
        SetListCountText();

        for(int i = 0; i < 3; i++)
            GameManager.Instance.objectPool.Generate("QuestListSlot", false);
    }

    public void AddQuest(Quest _quest, QuestGiver questGiver) {
        QuestSlot slot = GameManager.Instance.objectPool.GetObject("QuestListSlot").GetComponent<QuestSlot>();

        slot.quest = _quest;
        slot.QuestGiverNPC = questGiver;
        slot.Init();

        questSlots.Add(slot);
        SetSlotNums();
    }

    public void RemoveQuest(Quest _quest) {
        QuestSlot slotToRemove = GetQuestSlot(_quest);

        slotToRemove.RemoveQuestSlot();
        questSlots.Remove(slotToRemove);

        if(beforeSlot != null && beforeSlot.Equals(slotToRemove)) {
            beforeSlot = null;
            npcImg.enabled = false;
            npcNameText.text = "";
            titleText.text = "";
            contentText.text = "";
        }

        SetSlotNums();
    }

    public void ShowQuestContent(QuestSlot slot) {
        if(beforeSlot != null)
            beforeSlot.Selected = false;

        beforeSlot = slot;
        npcImg.enabled = true;
        npcImg.sprite = slot.QuestGiverNPC.npcSprite;
        npcNameText.text = slot.QuestGiverNPC.npcName;
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

    public void UpdateObjectives(Quest _quest) {
        QuestSlot questSlot = null;
        foreach(var slot in questSlots) {
            if(slot.quest.Equals(_quest))
                questSlot = slot;
        }

        foreach(var obj in questSlot.quest.collectObjectives)
            obj.UpdateItemCount();
        questSlot.CheckCompletable();
    }

    public void SetObjectivesUI() {
        for(int i = 0; i < objectives.Length; i++)
            objectives[i].SetActive(false);

        if(beforeSlot == null)
            return;

        CollectObjective[] obj = beforeSlot.quest.collectObjectives;
        for(int i = 0; i < beforeSlot.quest.collectObjectives.Length; i++) {
            objectives[i].SetActive(true);

            Image objectiveItem = objectives[i].GetComponent<Image>();
            objectiveItem.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            objectiveItem.sprite = obj[i].item.icon;

            Text objectiveText = objectiveItem.transform.GetChild(0).GetComponent<Text>();
            objectiveText.text = obj[i].item.name + "\n" + obj[i].currentAmount + "/" + obj[i].amount;
        }
    }

    public void SetListCountText() {
        if(beforeSlot != null)
            listCountText.text = beforeSlot.slotNum + 1 + " / " + questSlots.Count;
        else
            listCountText.text = "0 / " + questSlots.Count;
    }

    private QuestSlot GetQuestSlot(Quest _quest) {
        foreach(var slot in questSlots) {
            if(slot.quest.Equals(_quest)) {
                return slot;
            }
        }
        return null;
    }

    private void SetSlotNums() {
        for(int i = 0; i < questSlots.Count; i++) {
            questSlots[i].slotNum = i;
        }
    }
}