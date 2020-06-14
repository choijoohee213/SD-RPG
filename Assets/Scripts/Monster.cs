using System.Collections;
using UnityEngine;

public class Monster : FSMBase {
    [SerializeField]
    int Health;

    [SerializeField]
    private Player player;

    float DistanceFromPlayer {
        get {
            return Vector3.Distance(transform.position, player.transform.position);
        }
    }
    Vector3 PlayerFrontPos {
        get {
            return new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z + 2f);
        }
    }

    protected override void Awake() {
        base.Awake();
    }

    protected IEnumerator Idle() {
        do {
            yield return null;
            if(DistanceFromPlayer <= 10)
                SetState(CharacterState.Trace);
            else if(Random.Range(1, 20) == 2)
                SetState(CharacterState.Walk);

        } while(!isNewState);
    }

    protected IEnumerator Walk() {
        Vector3 movePos = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
        do {
            yield return null;
            MoveController.MoveControl(transform, movePos);

            //플레이어와의 거리가 15이하라면 Trace 상태로 변경
            if(DistanceFromPlayer <= 10)
                SetState(CharacterState.Trace);

            //랜덤한 확률로 Idle 상태로 변경
            else if(Random.Range(1,30) == 2)
                SetState(CharacterState.Idle);

        } while(!isNewState);
    }

    protected IEnumerator Trace() {
        do {
            yield return null;
            MoveController.LookTarget(transform, player.transform, 3f);
            MoveController.MoveToTarget(transform, PlayerFrontPos, 5f);

            if(DistanceFromPlayer > 10)
                SetState(CharacterState.Idle);
            else if(transform.position.Equals(PlayerFrontPos))
                SetState(CharacterState.Attack);
        } while(!isNewState); //do 문 종료조건.
    }

    protected IEnumerator Attack() {
        do {
            yield return null;
            MoveController.LookTarget(transform, player.transform, 3f);

            if(!transform.position.Equals(PlayerFrontPos))
                SetState(CharacterState.Trace);
        } while(!isNewState); //do 문 종료조건.
    }
}