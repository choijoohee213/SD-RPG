using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossQuest : Singleton<BossQuest> {
    public Quest KillBossQuest;

    public GameObject Player, Fence, FenceBroken, EntranceOpened, EntranceClosed;
    public GameObject BossCastleUI, ExitPanel;

    public bool QuestProgressing { get; set; }

    private Vector3 BridgeEntrancePos = new Vector3(4.3f, 20f, -7.8f);

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
