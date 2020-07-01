using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    public Camera Cam;
    public ObjectPool objectPool;
    public PlayerBase player;

    private GameObject activeMenuObj;

    [Header("UI")]
    public Joystick joystick;
    public Text playerLevelText,countDownText;
    public GameObject resurrectUI, InventoryUI, ItemDetailsUI, QuestUI;

    // Start is called before the first frame update
    private void Awake() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    public void UITransition(GameObject newMenuObj) {
        if(activeMenuObj != null && !activeMenuObj.Equals(newMenuObj))
            activeMenuObj.SetActive(false);

            activeMenuObj = newMenuObj;        
    }

    public void OnInventoryBtn() {
        InventoryUI.SetActive(!InventoryUI.activeSelf);
        ItemDetailsUI.SetActive(false);
        
        UITransition(InventoryUI);
    }

    public void OnQuestBtn() {
        QuestUI.SetActive(!QuestUI.activeSelf);
        QuestUIScript.Instance.ChangeCountText();
        UITransition(QuestUI);
    }

}