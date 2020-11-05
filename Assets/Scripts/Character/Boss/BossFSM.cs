using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : CharacterFSM
{
    BossBase boss;
    WaitForSeconds waitForSeconds = new WaitForSeconds(4f);

    protected override void Awake() {
        base.Awake();
        boss = GetComponent<BossBase>();

    }

    protected IEnumerator Idle() {
        do {
            yield return null;
            if(BossQuest.Instance.OnFighting) {
                yield return waitForSeconds;
                SetState((CharacterState) Random.Range(5, 6));
            } 
        } while(!IsNewState); //do 문 종료조건.
    }

    protected IEnumerator Walk() {
        do {
            yield return null;           
        } while(!IsNewState);
    }

    protected IEnumerator Attack_Jump() {
        Vector3 targetPos = player.transform.position;
        Vector3 target = new Vector3(targetPos.x, transform.position.y, targetPos.z);
        float distance = Vector3.Distance(target, transform.position);
        transform.LookAt(target);

        do {
            yield return null;
            if(transform.position != target) {
                transform.position = Vector3.MoveTowards(transform.position, target,  distance * Time.deltaTime);
                
            }
            else
                SetState(CharacterState.Idle);
        } while(!IsNewState);
    }




    protected IEnumerator Attack_Fire() {
        do {
            yield return null;
            if(boss.AttackJumping) {
                yield return waitForSeconds;
                SetState(CharacterState.Attack_Jump);
            }
        } while(!IsNewState);
    }

    protected IEnumerator Dizzy() {
        do {
            yield return null;
        } while(!IsNewState);
    }

}
