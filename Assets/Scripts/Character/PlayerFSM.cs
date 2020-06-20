using System.Collections;
using UnityEngine;

public class PlayerFSM : CharacterFSM {
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
            MoveController.MoveControl(transform, playerBase.MovePos, 10);

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

    protected IEnumerator Die() {
        do {
            yield return null;
        } while(!isNewState); //do 문 종료조건.
    }
}