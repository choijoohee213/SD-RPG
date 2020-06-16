using System.Collections;
using UnityEngine;

public class Player : FSMBase {
    private float speed = 10;
    private bool isJumping, isAttack, attackEvent;
    public bool IsJumping { get => isJumping; }

    private Vector3 movePos;

    [SerializeField]
    private Joystick joystick;

    [SerializeField]
    ParticleSystem[] hitEffect;



    protected override void Awake() {
        base.Awake();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }


    private void FixedUpdate() {
        if(state != CharacterState.Attack && !joystick.IsPointerUp)
            movePos = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        //movePos = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    }

    //모든 개체는 Idle 상태를 가진다.
    protected IEnumerator Idle() {
        do {
            yield return null;

            if(!joystick.IsPointerUp)
                SetState(CharacterState.Walk);
            if(isAttack)
                SetState(CharacterState.Attack);
        } while(!isNewState); //do 문 종료조건.
    }


    protected IEnumerator Walk() {
        do {
            yield return null;
            MoveController.MoveControl(transform, movePos);

            if(joystick.IsPointerUp)
                SetState(CharacterState.Idle);
            if(isAttack)
                SetState(CharacterState.Attack);
        } while(!isNewState);
    }

    protected IEnumerator Attack() {
        do {
            yield return null;
            Debug.DrawRay(transform.position + new Vector3(0,0.3f,0), transform.forward * 3f, Color.blue);
            
            if(Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), transform.forward, out RaycastHit hit, 3f, 1 << LayerMask.NameToLayer("Monster"))) {
                ParticleController.ModifyParticlesAwake(hitEffect, true);
                if(AttackEvent) {
                    Hit(hit.collider.GetComponent<FSMBase>());
                    AttackEvent = false;
                }
            }
            else {
                ParticleController.ModifyParticlesAwake(hitEffect, false);
            }

            if(!isAttack && joystick.IsPointerUp)
                SetState(CharacterState.Idle);
            else if(!isAttack && !joystick.IsPointerUp)
                SetState(CharacterState.Walk);
        } while(!isNewState);
    }

    public void PlayerAttack(bool _isAttack) {
        isAttack = _isAttack;
    }

    public void PlayerJump() {
        if(!isJumping) {
            Rigid.AddForce(Vector3.up *80, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        //플레이어가 플랫폼과 충돌할 때
        if(collision.gameObject.layer.Equals(8)) {
            isJumping = false;
        }
        if(collision.gameObject.layer.Equals(10)) {

        }
    }
}