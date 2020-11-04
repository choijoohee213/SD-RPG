using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : CharacterFSM
{
    
    protected IEnumerator Idle() {
        do {
            yield return null;
            if(BossQuest.Instance.OnFighting) {
                SetState((CharacterState) Random.Range(5, 7));
            } 
        } while(!IsNewState); //do 문 종료조건.
    }

    protected IEnumerator Walk() {
        do {
            yield return null;           
        } while(!IsNewState);
    }

    protected IEnumerator Attack_Jump() {
        do {
            yield return null;
        } while(!IsNewState);
    }

    protected IEnumerator Attack_Fire() {
        do {
            yield return null;
        } while(!IsNewState);
    }

    protected IEnumerator Dizzy() {
        do {
            yield return null;
        } while(!IsNewState);
    }

}
