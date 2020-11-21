using System.Collections;

public class PlayerFSM : CharacterFSM {
    private float moveSpeed = 10f;
    private string layerName;
    public bool BossAttackable { get; set; }

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
            if(BossQuest.Instance.OnFighting && BossAttackable)
                layerName = "Boss";
            else
                layerName = "Monster";

            bool raycastTarget = playerBase.AttackToTarget(layerName);

            if(!playerBase.AttackBtnPressed && Joystick.IsPointerUp || playerBase.IsColliderDie)
                SetState(CharacterState.Idle);
            else if(!playerBase.AttackBtnPressed && !Joystick.IsPointerUp)
                SetState(CharacterState.Walk);
        } while(!IsNewState);
    }
}