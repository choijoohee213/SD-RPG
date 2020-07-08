using UnityEngine;

public class QuestGiver : NPC {
    private QuestGiverUIScript giverUIScript;
    public Quest[] QuestList;

    [TextArea(2, 6)]
    public string defaultDialog;

    private bool noQuest;

    private int questIndex;

    private int QuestIndex {
        get { return questIndex; }
        set {
            if(value >= QuestList.Length)
                noQuest = true;
            else
                noQuest = false;
            questIndex = value;
        }
    }

    protected override void Awake() {
        base.Awake();
        QuestIndex = 0;
        giverUIScript = NPCUIScript.Instance.questGiverUIScript;
    }

    public override void ShowNPCUI() {
        Quest curQuest = QuestList[questIndex];
        giverUIScript.QuestGiverUI.SetActive(true);

        if(!noQuest) {
            if(curQuest.state.Equals(QuestState.Startable))
                giverUIScript.DisabledRewardsUI();
            if(curQuest.state.Equals(QuestState.Progressing))
                QuestUIScript.Instance.UpdateAllObjectives();
            if(curQuest.state.Equals(QuestState.Completable))
                giverUIScript.SetRewardsUI(curQuest);

            print(curQuest.state);
            giverUIScript.SetDialog(curQuest);
        }
        else
            giverUIScript.SetDialog(null);

        CheckQuestState();
    }

    public void AcceptQuest() {
        QuestUIScript.Instance.AddQuest(QuestList[QuestIndex]);
    }

    public void AbandonQuest() {
        QuestUIScript.Instance.RemoveQuest(QuestList[QuestIndex]);
        QuestIndex++;
    }

    public void CompleteQuest() {
        if(QuestList[questIndex].rewards.Reward()) {
            RemoveObjectivesItem();
            QuestList[QuestIndex].state = QuestState.Complete;
            QuestUIScript.Instance.RemoveQuest(QuestList[QuestIndex]);
            QuestIndex++;
        }
    }

    private void RemoveObjectivesItem() {
        foreach(var o in QuestList[QuestIndex].collectObjectives) {
            Inventory.Instance.RemoveMultiple(o.item, o.amount);
        }
    }

    private void CheckQuestState() {
        QuestState questState = 0;
        if(!noQuest)
            questState = QuestList[QuestIndex].state;

        giverUIScript.AcceptBtn.SetActive(!noQuest && questState.Equals(QuestState.Startable));
        giverUIScript.AbandonmentBtn.SetActive(!noQuest && questState.Equals(QuestState.Progressing));
        giverUIScript.CompleteBtn.SetActive(!noQuest && questState.Equals(QuestState.Completable));
    }
}