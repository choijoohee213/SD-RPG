using UnityEngine;

public abstract class NPC : Interactable {
    private NPCUIScript npcUIScript;

    public string npcName;
    public Sprite npcSprite;
    public SpriteRenderer speechBubble;

    protected override void Awake() {
        base.Awake();
        radius = 2.3f;
        npcUIScript = NPCUIScript.Instance;
        speechBubble.gameObject.SetActive(false);
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
        npcUIScript.NPCUI.SetActive(true);
        npcUIScript.npcImg.sprite = npcSprite;
        npcUIScript.npcNameText.text = npcName;
    }
}