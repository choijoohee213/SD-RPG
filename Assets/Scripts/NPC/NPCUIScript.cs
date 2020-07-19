using UnityEngine;
using UnityEngine.UI;

public class NPCUIScript : Singleton<NPCUIScript> {
    public GameObject NPCUI;
    public NPC InteractableNPC { get; set; }
    public Image npcImg;
    public Text npcNameText;

    public QuestGiverUIScript questGiverUIScript;

    public GameObject StartTalkingBtn;

    public void OnStartTalkingBtn() {
        StartTalkingBtn.SetActive(false);
        InteractableNPC.ShowNPCUI();
    }

    public void OnNPCUIExitBtn() {
        InteractableNPC.HasInteracted = false;

        NPCUI.SetActive(false);
        questGiverUIScript.QuestGiverUI.SetActive(false);
    }
}