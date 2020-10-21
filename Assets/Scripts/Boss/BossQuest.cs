using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossQuest : Singleton<BossQuest>
{
    public Quest KillBossQuest;
    public GameObject Fence;
    public GameObject FenceBroken;

    //막혀있던 보스 길을 여는 함수  
    public void OpenBridge() {
        Fence.SetActive(false);
        FenceBroken.SetActive(true);
    } 
}
