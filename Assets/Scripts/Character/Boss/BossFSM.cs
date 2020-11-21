using System.Collections;
using UnityEngine;

public class BossFSM : CharacterFSM {
    private BossBase boss;
    private PlayerFSM playerFSM;
    private readonly WaitForSeconds waitForSeconds = new WaitForSeconds(4f);

    private Vector3 targetPos;
    private float distance;
    private int attackCount = 0;

    protected override void Awake() {
        base.Awake();
        boss = GetComponent<BossBase>();
        playerFSM = playerBase.GetComponent<PlayerFSM>();
    }

    protected IEnumerator Idle() {
        do {
            yield return null;
            if(BossQuest.Instance.OnFighting && !playerBase.IsDie) {
                if(attackCount != 3) {
                    yield return waitForSeconds;
                    attackCount++;
                    SetState((CharacterState)Random.Range(5, 7));
                }
                else {
                    attackCount = 0;
                    SetState(CharacterState.Dizzy);
                }
            }
        } while(!IsNewState); //do 문 종료조건.
    }

    protected IEnumerator Walk() {
        do {
            yield return null;
        } while(!IsNewState);
    }

    protected IEnumerator Attack_Jump() {
        targetPos = new Vector3(playerBase.transform.position.x, transform.position.y, playerBase.transform.position.z);
        distance = Vector3.Distance(targetPos, transform.position);
        transform.LookAt(targetPos);

        do {
            yield return null;
            if(playerBase.IsDie || !BossQuest.Instance.OnFighting)
                SetState(CharacterState.Idle);

            if(transform.position != targetPos)
                transform.position = Vector3.MoveTowards(transform.position, targetPos, distance * Time.deltaTime);
            else
                SetState(CharacterState.Idle);
        } while(!IsNewState);
    }

    protected IEnumerator Attack_Fire() {
        transform.LookAt(playerBase.transform.position);
        boss.AttackStart = true;
        boss.attackFireCount = 0;

        do {
            yield return null;
            if(playerBase.IsDie || !BossQuest.Instance.OnFighting)
                SetState(CharacterState.Idle);

            if(boss.AttackStart == false) {
                yield return new WaitForSeconds(2f);
                SetState(CharacterState.Idle);
            }
        } while(!IsNewState);
    }

    protected IEnumerator Dizzy() {
        do {
            playerFSM.BossAttackable = true;
            yield return new WaitForSeconds(10f);
            playerFSM.BossAttackable = false;
            SetState(CharacterState.Idle);
        } while(!IsNewState);
    }
}