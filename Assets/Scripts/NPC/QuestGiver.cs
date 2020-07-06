
using UnityEngine;

public class QuestGiver : NPC
{
    QuestGiverUIScript UIScript;
    public Quest[] QuestList;

    [TextArea(2,6)]
    public string defaultDialog;

    bool noQuest;

    int questIndex;
    int QuestIndex {
        get { return questIndex; }
        set {
            if(value >= QuestList.Length) noQuest = true;
            else noQuest = false;
            questIndex = value;
        } 
    }


    protected override void Awake() {
        base.Awake();
        QuestIndex = 0;
        UIScript = NPCUIScript.Instance.questGiverUIScript;
    }


    public override void ShowNPCUI() {
        UIScript.QuestGiverUI.SetActive(true);
        
        if(!noQuest) {
            UIScript.QuestGiverContentText.text = QuestList[QuestIndex].title + "\n\n" + QuestList[QuestIndex].content;

            if(QuestList[QuestIndex].state.Equals(QuestState.Progressing))
                QuestUIScript.Instance.UpdateAllObjectives();
        }
        else 
            UIScript.QuestGiverContentText.text = defaultDialog;

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
        QuestUIScript.Instance.RemoveQuest(QuestList[QuestIndex]);
        QuestList[questIndex].rewards.Reward();
        QuestIndex++;
    }

    private void CheckQuestState() {
        QuestState questState = 0;
        if(!noQuest) questState = QuestList[QuestIndex].state;

        UIScript.AcceptBtn.SetActive(!noQuest && questState.Equals(QuestState.Startable));
        UIScript.AbandonmentBtn.SetActive(!noQuest && questState.Equals(QuestState.Progressing));
        UIScript.CompleteBtn.SetActive(!noQuest && questState.Equals(QuestState.Completable));
    }

}
