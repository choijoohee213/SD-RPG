using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : CharacterFSM
{
    BossBase boss;
    WaitForSeconds waitForSeconds = new WaitForSeconds(4f);

    Vector3 targetPos;
    float distance;

    public GameObject box;
    Rigidbody r;

    protected override void Awake() {
        base.Awake();
        boss = GetComponent<BossBase>();
        r = box.GetComponent<Rigidbody>();
    }

    protected IEnumerator Idle() {
        do {
            yield return null;
            if(BossQuest.Instance.OnFighting) {
                yield return waitForSeconds;
                SetState((CharacterState) Random.Range(6, 7));
            } 
        } while(!IsNewState); //do 문 종료조건.
    }

    protected IEnumerator Walk() {
        do {
            yield return null;           
        } while(!IsNewState);
    }

    protected IEnumerator Attack_Jump() {
        targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        distance = Vector3.Distance(targetPos, transform.position);
        transform.LookAt(targetPos);

        do {
            yield return null;
            if(transform.position != targetPos) {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, distance * Time.deltaTime);

            }
            else {
                SetState(CharacterState.Idle);
            }
        } while(!IsNewState);
    }



    
    protected IEnumerator Attack_Fire() {
        Transform targetTrans = player.transform;
        transform.LookAt(targetTrans.position);

        do {
            yield return null;
            boss.CreateFireBall(targetTrans);
            SetState(CharacterState.Idle);
        } while(!IsNewState);
    }

    protected IEnumerator Dizzy() {
        do {
            yield return null;
        } while(!IsNewState);
    }

}
