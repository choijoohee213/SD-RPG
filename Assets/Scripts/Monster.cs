using System.Collections;
using UnityEngine;

public class Monster : FSMBase {
    [SerializeField]
    private Player player;

    private Vector3 movePos;

    float DistanceFromPlayer {
        get {
            return Vector3.Distance(transform.position, player.transform.position);
        }
    }

    //************************나중에 지워라!!!!*****************//
    private void Update() {
        Debug.DrawRay(transform.position + new Vector3(0, 0.3f, 0), transform.forward * 1.5f, Color.blue);
        Debug.DrawRay(transform.position + new Vector3(0, 0.2f, 0), transform.right * 1.5f, Color.blue);
        Debug.DrawRay(transform.position + new Vector3(0, 0.2f, 0), -transform.right * 1.5f, Color.blue);
    }

    protected override void Awake() {
        base.Awake();
        
    }

    protected IEnumerator Idle() {
        do {
            yield return null;

            //플레이어와의 거리가 10 이하이고,높이차가 1 미만일 경우 Trace 상태로 전환
            if(DistanceFromPlayer <= 10 && player.transform.position.y - transform.position.y < 1)
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
            MoveController.MoveControl(transform, movePos, 8);

            //플레이어와의 거리가 10 이하이고, 높이차가 1 미만일 경우 Trace 상태로 전환
            if(DistanceFromPlayer <= 10 && player.transform.position.y - transform.position.y < 1)
                SetState(CharacterState.Trace);

            //랜덤한 확률로 Idle 상태로 전환
            else if(Random.Range(1, 30) == 2)
                SetState(CharacterState.Idle);

        } while(!isNewState);
    }

    protected IEnumerator Trace() {
        do {
            yield return null;

            if(!player.IsJumping) {
                MoveController.LookTarget(transform, player.transform, 3f);
                MoveController.RigidMovePos(transform, Rigid, player.transform.position - transform.position, 8);
            }

            //플레이어와의 거리가 10 이상이고, 높이차이가 1이상일 경우 Idle 상태로 전환
            if(DistanceFromPlayer > 10 && player.transform.position.y - transform.position.y >= 1)
                SetState(CharacterState.Idle);

            ////플레이어와 충돌했을 경우 Attack 상태로 전환
            if(Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), transform.forward, out RaycastHit hit, 1.5f, 1 << LayerMask.NameToLayer("Player"))
                || Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), transform.right, 1.5f, 1 << LayerMask.NameToLayer("Player"))
                || Physics.Raycast(transform.position + new Vector3(0f, 0.2f, 0), -transform.right, 1.5f, 1 << LayerMask.NameToLayer("Player"))) {
                SetState(CharacterState.Attack);
            }

        } while(!isNewState); //do 문 종료조건.
        
    }


    protected IEnumerator Attack() {
        do {
            yield return null;
            MoveController.LookTarget(transform, player.transform, 3f);
            
            

            if(!Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), transform.forward, out RaycastHit hit, 1.5f, 1 << LayerMask.NameToLayer("Player"))) {
                SetState(CharacterState.Trace);
            }
            else {
                if(AttackEvent) {
                    hit.collider.GetComponent<FSMBase>().TakeDamage(Damage);
                    AttackEvent = false;
                }
            }
        } while(!isNewState); //do 문 종료조건.
    }

}