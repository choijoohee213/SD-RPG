using UnityEngine;
using UnityEngine.UI;

public class QuestGiverUIScript : MonoBehaviour
{
    NPCUIScript nPCUIScript;
    public GameObject QuestGiverUI, AcceptBtn, AbandonmentBtn, CompleteBtn;
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
}
