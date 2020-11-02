﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossQuest : Singleton<BossQuest> {
    public Quest KillBossQuest;
    public EventCamera eventCamera;

    public GameObject Player, Fence, FenceBroken, EntranceOpened, EntranceClosed;

    public bool OnQuest { get; set; }
    public bool OnAnimation { get; set; }

    private Vector3 BridgeEntrancePos = new Vector3(-9.6f, 22f, 86f);

    //막혀있던 보스 길을 여는 함수  
    public void OpenBridge() {
        OnQuest = true;
        Fence.SetActive(false);
        FenceBroken.SetActive(true);
    } 
 
    public void EnterCastle() {
        UIManager.Instance.BossExitPanel.SetActive(false);
        EntranceOpened.SetActive(false);
        EntranceClosed.SetActive(true);

        OnAnimation = true;
        UIManager.Instance.OnOffCanvas(false, false, true);
        UIManager.Instance.joystick.PointerUp();

        eventCamera.StartAnimation();
    }

    private void ExitCastle() {
        UIManager.Instance.OnOffCanvas(true, true, false);
        EntranceOpened.SetActive(true);
        EntranceClosed.SetActive(false);
    }

    public void OnExitBtn() {
        UIManager.Instance.BossExitPanel.SetActive(true);
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
