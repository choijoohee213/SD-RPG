using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : FSMBase
{
    float speed = 10;
    bool isJumping;
    Vector3 movePos;

    [SerializeField]
    Joystick joystick;

    protected override void Awake()
    {
        base.Awake();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    protected override void OnEnable() {
        base.OnEnable();
    }

    void FixedUpdate() {
        PlayerMove();
    }

    void PlayerMove() {
        if(state == CharacterState.Attack) return;
        //movePos = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movePos = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        if(movePos.x != 0 || movePos.z != 0) {
            SetState(CharacterState.Run);
            
        }
    }

    protected override IEnumerator Run() {
        do {
            transform.rotation = Quaternion.LookRotation(movePos.x * Vector3.right + movePos.z * Vector3.forward);
            transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);

            if(movePos.x == 0 && movePos.z == 0)
                SetState(CharacterState.Idle);
            yield return null;

        } while(!isNewState); 
    }


    public void PlayerJump() {
        if(!isJumping) {
            rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
            isJumping = true;
        }
    }
 

    public void PlayerAttack(bool _isAttack) {
        if(_isAttack) SetState(CharacterState.Attack);
        else SetState(CharacterState.Idle);
    }

    void OnCollisionEnter(Collision collision) {
        //플레이어가 플랫폼과 충돌할 때
        if(collision.gameObject.layer.Equals(8)) {  
            isJumping = false;
        }
    }
}
