using UnityEngine;

public class QuestGiver : NPC {
    private QuestGiverUIScript questGiverUIScript;
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
        SetQuestIndex();
        questGiverUIScript = NPCUIScript.Instance.questGiverUIScript;
        SetSpeechBubble();
    }

    private void SetQuestIndex() {
        int index = 0;
        for(int i = 0; i < QuestList.Length; i++) {
            if(!QuestList[i].state.Equals(QuestState.Complete)) {
                index = i;
                break;
            }
            if(i.Equals(QuestList.Length - 1)) {
                index = QuestList.Length;
            }
        }
        QuestIndex = index;
    }

    public override void ShowNPCUI() {
        base.ShowNPCUI();

        questGiverUIScript.QuestGiverUI.SetActive(true);
        questGiverUIScript.DisabledRewardsUI();

        if(!noQuest) {
            Quest curQuest = QuestList[questIndex];
            if(curQuest.state.Equals(QuestState.Progressing))
                QuestUIScript.Instance.UpdateObjectives(curQuest);
            if(curQuest.state.Equals(QuestState.Completable)) {
                if(curQuest.rewards.ItemReward != null)
                    questGiverUIScript.SetRewardsUI(curQuest);
            }

            questGiverUIScript.SetDialog(curQuest);
        }
        else
            questGiverUIScript.SetDialog(null);

        CheckQuestState();
    }

    public void AcceptQuest() {
        QuestUIScript.Instance.AddQuest(QuestList[QuestIndex], this);
        QuestUIScript.Instance.UpdateObjectives(QuestList[QuestIndex]);
        SetSpeechBubble();

        //보스처치 퀘스트인지 확인
        if(QuestList[QuestIndex].Equals(BossQuest.Instance.KillBossQuest))
            BossQuest.Instance.StartQuest();
    }

    public void CompleteQuest() {
        if(QuestList[questIndex].rewards.Reward()) {
            RemoveObjectivesItem();
            QuestList[QuestIndex].state = QuestState.Complete;
            QuestUIScript.Instance.RemoveQuest(QuestList[QuestIndex]);
            QuestIndex++;
            SetSpeechBubble();
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

        questGiverUIScript.AcceptBtn.SetActive(!noQuest && questState.Equals(QuestState.Startable));
        questGiverUIScript.CompleteBtn.SetActive(!noQuest && questState.Equals(QuestState.Completable));
    }

    public void SetSpeechBubble() {
        if(!noQuest) {
            speechBubble.gameObject.SetActive(true);
            switch(QuestList[QuestIndex].state) {
                case QuestState.Startable:
                    speechBubble.sprite = questGiverUIScript.speechBubles[0];
                    break;

                case QuestState.Progressing:
                    speechBubble.sprite = questGiverUIScript.speechBubles[1];
                    break;

                case QuestState.Completable:
                    speechBubble.sprite = questGiverUIScript.speechBubles[2];
                    break;
            }
        }
        else {
            speechBubble.gameObject.SetActive(false);
        }
    }
}