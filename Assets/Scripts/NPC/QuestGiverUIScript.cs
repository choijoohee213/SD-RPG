using UnityEngine;
using UnityEngine.UI;

public class QuestGiverUIScript : NPCUIScript
{
    
    public GameObject QuestGiverUI, AcceptBtn, AbandonmentBtn, CompleteBtn;
    public Text QuestGiverContentText;
    
    public void OnQuestAcceptBtn() {
        InteractableNPC.GetComponent<QuestGiver>().AcceptQuest();

    }
}
