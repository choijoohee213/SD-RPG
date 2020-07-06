using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : Interactable
{
    NPCUIScript npcUIScript;

    protected override void Awake() {
        base.Awake();
        radius = 2.3f;
        npcUIScript = NPCUIScript.Instance;
    }

    public override void Interact() {
        npcUIScript.StartTalkingBtn.SetActive(true);
        npcUIScript.InteractableNPC = this;
        HasInteracted = true;
        
    }

    public override void NonInteract() {
        npcUIScript.StartTalkingBtn.SetActive(false);
        HasInteracted = false;
    }
   

    public virtual void ShowNPCUI() {

    }
}
