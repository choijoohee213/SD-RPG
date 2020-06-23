using System.Collections;
using UnityEngine;

public class PlayerFSM : CharacterFSM {
    private float moveSpeed = 10f;
    private PlayerBase playerBase;

    protected override void Awake() {
        base.Awake();
        playerBase = GetComponent<PlayerBase>();
    }

    //모든 개체는 Idle 상태를 가진다.
    protected IEnumerator Idle() {
        do {
            yield return null;
            if(!Joystick.IsPointerUp)
                SetState(CharacterState.Walk);
            else if(playerBase.IsAttack)
                SetState(CharacterState.Attack);
        } while(!isNewState); //do 문 종료조건.
    }

    protected IEnumerator Walk() {

        do {
            yield return null;
            MoveController.LookDirection(transform, playerBase.MovePos);
            MoveController.RigidMovePos(transform, playerBase.MovePos, moveSpeed);

            if(Joystick.IsPointerUp)
                SetState(CharacterState.Idle);
            if(playerBase.IsAttack)
                SetState(CharacterState.Attack);
        } while(!isNewState);
    }

    protected IEnumerator Attack() {
        do {
            yield return null;

            bool raycastTarget = playerBase.AttackToTarget("Monster");
            
            if(!playerBase.IsAttack && Joystick.IsPointerUp || playerBase.IsColliderDie)
                SetState(CharacterState.Idle);
            else if(!playerBase.IsAttack && !Joystick.IsPointerUp)
                SetState(CharacterState.Walk);
        } while(!isNewState);
    }

}