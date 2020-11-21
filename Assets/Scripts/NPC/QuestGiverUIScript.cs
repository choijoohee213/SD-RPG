using UnityEngine;
using UnityEngine.UI;

public class QuestGiverUIScript : MonoBehaviour {
    private NPCUIScript npcUIScript;

    public GameObject QuestGiverUI, AcceptBtn, CompleteBtn, rewardsEXP;
    public GameObject[] rewards;

    public Text QuestGiverContentText;
    public Sprite[] speechBubles;

    private void Awake() {
        npcUIScript = NPCUIScript.Instance;
    }

    public void OnQuestAcceptBtn() {
        npcUIScript.InteractableNPC.GetComponent<QuestGiver>().AcceptQuest();
    }

    public void OnQuestCompleteBtn() {
        npcUIScript.InteractableNPC.GetComponent<QuestGiver>().CompleteQuest();
    }

    public void SetRewardsUI(Quest quest) {
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
        if(quest != null && quest.state.Equals(QuestState.Completable))
            QuestGiverContentText.text = quest.title + "\n\n" + quest.completeDialog;
        else if(quest == null)
            QuestGiverContentText.text = npcUIScript.InteractableNPC.GetComponent<QuestGiver>().defaultDialog;
        else
            QuestGiverContentText.text = quest.title + "\n\n" + quest.content;
    }
}