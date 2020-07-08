using UnityEngine;
using UnityEngine.UI;

public class QuestGiverUIScript : MonoBehaviour {
    private NPCUIScript nPCUIScript;
    public GameObject QuestGiverUI, AcceptBtn, AbandonmentBtn, CompleteBtn, rewardsEXP;
    public GameObject[] rewards;
    public Text QuestGiverContentText;

    private void Awake() {
        nPCUIScript = NPCUIScript.Instance;
    }

    public void OnQuestAcceptBtn() {
        nPCUIScript.InteractableNPC.GetComponent<QuestGiver>().AcceptQuest();
    }

    public void OnQuestAbandonBtn() {
        nPCUIScript.InteractableNPC.GetComponent<QuestGiver>().AbandonQuest();
    }

    public void OnQuestCompleteBtn() {
        nPCUIScript.InteractableNPC.GetComponent<QuestGiver>().CompleteQuest();
    }

    public void SetRewardsUI(Quest quest) {
        DisabledRewardsUI();

        rewards[0].SetActive(true);
        Image itemImg = rewards[0].GetComponent<Image>();
        itemImg.sprite = quest.rewards.ItemReward.icon;

        Text itemText = itemImg.transform.GetChild(0).GetComponent<Text>();
        itemText.text = "<color=#0057EF>" + quest.rewards.ItemReward.name + "</color>   " + quest.rewards.ItemRewardCount + "개";

        rewardsEXP.SetActive(true);
        rewardsEXP.transform.GetChild(0).GetComponent<Text>().text = "<color=yellow>경험치</color>   " + quest.rewards.EXPReward.ToString() + " EXP";
    }

    public void DisabledRewardsUI() {
        for(int i = 0; i < rewards.Length; i++)
            rewards[i].SetActive(false);
        rewardsEXP.SetActive(false);
    }

    public void SetDialog(Quest quest) {
        if(quest.state.Equals(QuestState.Completable))
            QuestGiverContentText.text = quest.title + "\n\n" + quest.completeDialog;
        else if(quest == null)
            QuestGiverContentText.text = nPCUIScript.InteractableNPC.GetComponent<QuestGiver>().defaultDialog;
        else
            QuestGiverContentText.text = quest.title + "\n\n" + quest.content;
    }
}