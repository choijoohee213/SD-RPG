using System.Collections;

public class PlayerFSM : CharacterFSM {
    private float moveSpeed = 10f;

    //모든 개체는 Idle 상태를 가진다.
    protected IEnumerator Idle() {
        do {
            yield return null;
            if(!Joystick.IsPointerUp)
                SetState(CharacterState.Walk);
            else if(player.AttackBtnPressed)
                SetState(CharacterState.Attack);
        } while(!IsNewState); //do 문 종료조건.
    }

    protected IEnumerator Walk() {
        do {
            yield return null;
            MoveController.LookDirection(transform, player.MoveDir);
            MoveController.RigidMovePos(transform, player.MoveDir, moveSpeed);

            if(Joystick.IsPointerUp)
                SetState(CharacterState.Idle);
            if(player.AttackBtnPressed)
                SetState(CharacterState.Attack);
        } while(!IsNewState);
    }

    protected IEnumerator Attack() {
        do {
            yield return null;

            bool raycastTarget = player.AttackToTarget("Monster");

            if(!player.AttackBtnPressed && Joystick.IsPointerUp || player.IsColliderDie)
                SetState(CharacterState.Idle);
            else if(!player.AttackBtnPressed && !Joystick.IsPointerUp)
                SetState(CharacterState.Walk);
        } while(!IsNewState);
    }
}