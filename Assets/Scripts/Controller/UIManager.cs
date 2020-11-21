using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {
    private GameObject activeMenuObj;

    [Header("Canvas")]
    public Canvas StaticCanvas;

    public Canvas DynamicCanvas;
    public Canvas BossCanvas;
    public Canvas HPCanvas;

    [Header("UI")]
    public Joystick joystick;

    public Text playerLevelText, countDownText, dialogText;
    public GameObject ResurrectUI, InventoryUI, ItemDetailsUI, QuestUI, BossUI, BossExitPanel, BossDialogUI, AnimationScreen;

    public void UITransition(GameObject newMenuObj) {
        if(activeMenuObj != null && !activeMenuObj.Equals(newMenuObj))
            activeMenuObj.SetActive(false);

        activeMenuObj = newMenuObj;
    }

    public void OnOffCanvas(bool staticCanvas, bool dynamicCanvas, bool bossCanvas) {
        StaticCanvas.enabled = staticCanvas;
        DynamicCanvas.enabled = dynamicCanvas;
        BossCanvas.enabled = bossCanvas;
    }

    public void OnInventoryBtn() {
        InventoryUI.SetActive(!InventoryUI.activeSelf);
        ItemDetailsUI.SetActive(false);

        UITransition(InventoryUI);
    }

    public void OnQuestBtn() {
        QuestUI.SetActive(!QuestUI.activeSelf);

        QuestUIScript.Instance.SetListCountText();
        QuestUIScript.Instance.SetObjectivesUI();

        UITransition(QuestUI);
    }
}