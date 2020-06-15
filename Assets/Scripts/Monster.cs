using System.Collections;
using UnityEngine;

public class Monster : FSMBase {
    [SerializeField]
    int Health;

    [SerializeField]
    private Player player;

    bool collisionWithPlayer;

    float DistanceFromPlayer {
        get {
            return Vector3.Distance(transform.position, player.transform.position);
        }
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
            MoveController.MoveControl(transform, movePos);

            //플레이어와의 거리가 10 이하이고, 높이차가 1 미만일 경우 Trace 상태로 전환
            if(DistanceFromPlayer <= 10 && player.transform.position.y - transform.position.y < 1)
                SetState(CharacterState.Trace);

            //랜덤한 확률로 Idle 상태로 전환
            else if(Random.Range(1,30) == 2)
                SetState(CharacterState.Idle);

        } while(!isNewState);
    }

    protected IEnumerator Trace() {
        do {
            yield return null;
            MoveController.LookTarget(transform, player.transform, 3f);
            transform.Translate(Vector3.forward * 10 * Time.fixedDeltaTime);

            //플레이어와의 거리가 10 이상이고, 높이차이가 1이상일 경우 Idle 상태로 전환
            if(DistanceFromPlayer > 10 && player.transform.position.y - transform.position.y >= 1)
                SetState(CharacterState.Idle);

            //플레이어와 충돌했을 경우 Attack 상태로 전환
            else if(collisionWithPlayer)
                SetState(CharacterState.Attack);
        } while(!isNewState); //do 문 종료조건.
        
    }

    protected IEnumerator Attack() {
        do {
            yield return null;
            MoveController.LookTarget(transform, player.transform, 3f);

            if(!collisionWithPlayer)
                SetState(CharacterState.Trace);
        } while(!isNewState); //do 문 종료조건.
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.layer.Equals(9)) {
            collisionWithPlayer = true;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if(collision.gameObject.layer.Equals(9)) {
            collisionWithPlayer = false;
        }
    }
}