using UnityEngine;

public class NPCUIScript : Singleton<NPCUIScript> {
    public NPC InteractableNPC { get; set; }

    public QuestGiverUIScript questGiverUIScript;

    public GameObject StartTalkingBtn;

    public void OnStartTalkingBtn() {
        StartTalkingBtn.SetActive(false);
        InteractableNPC.ShowNPCUI();
    }

    public void OnNPCUIExitBtn() {
        InteractableNPC.HasInteracted = false;
    }
}