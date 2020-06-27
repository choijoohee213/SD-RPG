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
            else if(playerBase.AttackBtnPressed)
                SetState(CharacterState.Attack);
        } while(!IsNewState); //do 문 종료조건.
    }

    protected IEnumerator Walk() {

        do {
            yield return null;
            MoveController.LookDirection(transform, playerBase.MoveDir);
            MoveController.RigidMovePos(transform, playerBase.MoveDir, moveSpeed);

            if(Joystick.IsPointerUp)
                SetState(CharacterState.Idle);
            if(playerBase.AttackBtnPressed)
                SetState(CharacterState.Attack);
        } while(!IsNewState);
    }

    protected IEnumerator Attack() {
        do {
            yield return null;

            bool raycastTarget = playerBase.AttackToTarget("Monster");
            
            if(!playerBase.AttackBtnPressed && Joystick.IsPointerUp || playerBase.IsColliderDie)
                SetState(CharacterState.Idle);
            else if(!playerBase.AttackBtnPressed && !Joystick.IsPointerUp)
                SetState(CharacterState.Walk);
        } while(!IsNewState);
    }

}