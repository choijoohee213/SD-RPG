using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 10;
    bool isJumping, isAttack;
    
    Animator anim;
    Vector3 movePos;
    Rigidbody rigid;

    [SerializeField]
    Joystick joystick;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        anim.SetBool("Walk", movePos.x != 0 || movePos.z != 0);
    }

    void FixedUpdate() {
        PlayerMove();
    }

    void PlayerMove() {
        if(isAttack) return;
        //movePos = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movePos = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        if(movePos.x != 0 || movePos.z != 0) {
            transform.rotation = Quaternion.LookRotation(movePos.x * Vector3.right + movePos.z * Vector3.forward);
            transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
        }
    }

    public void PlayerJump() {
        if(!isJumping) {
            rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
            isJumping = true;
        }
    }

    void PlayerAnimation() {
        
    }

    public void PlayerAttack(bool _isAttack) {
        isAttack = _isAttack;
        anim.SetBool("Attack", isAttack);
    }

    void OnCollisionEnter(Collision collision) {
        //플레이어가 플랫폼과 충돌할 때
        if(collision.gameObject.layer.Equals(8)) {  
            isJumping = false;
        }
    }
}
