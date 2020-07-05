

public class QuestGiver : NPC
{
    QuestGiverUIScript UIScript;
    public Quest[] QuestList;

    int questIndex;

    protected override void Awake() {
        base.Awake();
        questIndex = 0;
        UIScript = npcUIScript.gameObject.GetComponent<QuestGiverUIScript>();
    }


    public override void ShowNPCUI() {
        UIScript.QuestGiverUI.SetActive(true);
        UIScript.QuestGiverContentText.text = QuestList[questIndex].title + "\n\n" + QuestList[questIndex].content;

        if(QuestList[questIndex].state.Equals(QuestState.Progressing))
            QuestUIScript.Instance.UpdateAllObjectives();

        CheckQuestState();
    }


    public void AcceptQuest() {
        QuestUIScript.Instance.AddQuest(QuestList[questIndex]);
    }

    private void CheckQuestState() {
        QuestState questState = QuestList[questIndex].state;
        UIScript.AcceptBtn.SetActive(questState.Equals(QuestState.Startable));
        UIScript.AbandonmentBtn.SetActive(questState.Equals(QuestState.Progressing));
        UIScript.CompleteBtn.SetActive(questState.Equals(QuestState.Completable));

    }

}
