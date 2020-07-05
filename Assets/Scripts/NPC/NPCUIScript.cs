using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCUIScript : MonoBehaviour
{
    public NPC InteractableNPC { get; set; }
    public GameObject StartTalkingBtn;
    
    public void OnStartTalkingBtn() {
        StartTalkingBtn.SetActive(false);
        InteractableNPC.ShowNPCUI();
    }

    public void OnNPCUIExitBtn() {
        InteractableNPC.HasInteracted = false;
    }

    

}
