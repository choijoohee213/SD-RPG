using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossQuest : Singleton<BossQuest> {
    public Quest KillBossQuest;
    public EventCamera eventCamera;

    public GameObject Player, Fence, FenceBroken, EntranceOpened, EntranceClosed, BossCastleUI, ExitPanel;
    public GameObject[] ExclamationMarks;

    public bool QuestProgressing { get; set; }

    private Vector3 BridgeEntrancePos = new Vector3(-9.6f, 22f, 86f);

    //막혀있던 보스 길을 여는 함수  
    public void OpenBridge() {
        QuestProgressing = true;
        Fence.SetActive(false);
        FenceBroken.SetActive(true);
    } 
 
    public void EnterCastle() {
        BossCastleUI.SetActive(true);
        ExitPanel.SetActive(false);
        EntranceOpened.SetActive(false);
        EntranceClosed.SetActive(true);

        GameManager.Instance.objectPool.Canvas.gameObject.SetActive(false);
        GameManager.Instance.joystick.PointerUp();

        eventCamera.CameraMove();
    }

    private void ExitCastle() {
        BossCastleUI.SetActive(false);
        ExitPanel.SetActive(false);
        EntranceOpened.SetActive(true);
        EntranceClosed.SetActive(false);
    }

    public void OnExitBtn() {
        ExitPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnExitAnswerBtn(bool IsExit) {
        if(IsExit) {
            Player.transform.position = BridgeEntrancePos;
            ExitCastle();
        }

        Time.timeScale = 1f;
    }

}
