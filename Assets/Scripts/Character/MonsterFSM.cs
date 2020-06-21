using System.Collections;
using UnityEngine;

public class MonsterFSM : CharacterFSM {
    [SerializeField]
    private PlayerBase playerBase;
    private MonsterBase monsterBase;

    public float DistanceFromPlayer => Vector3.Distance(transform.position, playerBase.transform.position);
    
    bool PlayerInAttackRange => DistanceFromPlayer <= 10 && playerBase.transform.position.y - transform.position.y < 1 && !playerBase.IsDie
                 && playerBase.IsWithInRange(monsterBase.limitRange_Min, monsterBase.limitRange_Max);

    protected override void Awake() {
        base.Awake();
        monsterBase = GetComponent<MonsterBase>();
    }


    protected IEnumerator Idle() {

        do {
            yield return null;

            //플레이어와의 거리가 10 이하이고,높이차가 1 미만일 경우 Trace 상태로 전환
            if(PlayerInAttackRange && !playerBase.IsDie)
                SetState(CharacterState.Trace);

            //랜덤한 확률로 Walk 상태로 전환
            else if(Random.Range(1, 20) == 2)
                SetState(CharacterState.Walk);
        } while(!isNewState);
    }

    protected IEnumerator Walk() {
        Vector3 movePos = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));

        do {
            yield return null;
            MoveController.MoveDir(transform, movePos, 8f);
            MoveController.LimitMoveRange(transform, monsterBase.limitRange_Min, monsterBase.limitRange_Max);

            //플레이어와의 거리가 10 이하이고, 높이차가 1 미만일 경우 Trace 상태로 전환
            if(PlayerInAttackRange && !playerBase.IsDie)
                SetState(CharacterState.Trace);

            //랜덤한 확률로 Idle 상태로 전환
            else if(Random.Range(1, 30) == 2)
                SetState(CharacterState.Idle);
        } while(!isNewState);
    }

    protected IEnumerator Trace() {
        do {
            yield return null;
            if(!playerBase.IsJumping){
                MoveController.LookTarget(transform, playerBase.transform, 3f);
                MoveController.RigidMovePos(transform, playerBase.transform.position - transform.position, 8f);
                MoveController.LimitMoveRange(transform, monsterBase.limitRange_Min, monsterBase.limitRange_Max);                
            }

            //플레이어와의 거리가 10 이상이거나 높이차이가 1이상일 경우 Idle 상태로 전환
            if(!PlayerInAttackRange)
                SetState(CharacterState.Idle);

            ////플레이어와 충돌했을 경우 Attack 상태로 전환
            if(characterBase.CheckRaycastHit("Player") && !characterBase.IsColliderDie)
                SetState(CharacterState.Attack);
        } while(!isNewState); //do 문 종료조건.
    }

    protected IEnumerator Attack() {
        do {
            yield return null;
            MoveController.LookTarget(transform, playerBase.transform, 3f);

            bool raycastTarget = characterBase.AttackToTarget("Player");
            if(!raycastTarget){ 
                if(playerBase.IsDie)
                    SetState(CharacterState.Walk);
                else
                    SetState(CharacterState.Trace);
            }
        } while(!isNewState); //do 문 종료조건.
    }

    protected IEnumerator Die() {
        do {
            yield return null;
        } while(!isNewState); //do 문 종료조건.
    }
}