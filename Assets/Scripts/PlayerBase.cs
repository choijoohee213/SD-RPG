using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : CharacterBase {
    private Vector3 movePos;
    public Vector3 MovePos { get => movePos; }

    public Vector3 StartPos { get { return new Vector3(74, 21.79191f, 44); } }

    [Header("UI")]
    [SerializeField]
    private Joystick joystick;

    [SerializeField]
    private GameObject ResurrectUI;

    [SerializeField]
    private Text CountdownText;

    private int resurrectCountDown = 5;

    private bool isJumping, isAttack, attackEvent;
    public bool IsJumping { get => isJumping; }
    public bool IsAttack { get => isAttack; }

    private void FixedUpdate() {
        if(GetComponent<CharacterFSM>().state != CharacterState.Attack && !Joystick.IsPointerUp)
            movePos = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        //movePos = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    public void PlayerJump() {
        if(!isJumping) {
            Rigid.AddForce(Vector3.up * 80, ForceMode.Impulse);
            isJumping = true;
        }
    }

    public void PlayerAttackBtn(bool _isAttack) {
        isAttack = _isAttack;
    }

    protected override void DieAnimEvent() {
        gameObject.SetActive(false);
        ResurrectUI.SetActive(true);
        ResurrectTimer();
    }

    private void ResurrectTimer() {
        CountdownText.text = resurrectCountDown.ToString();
        if(resurrectCountDown-- != 0)
            Invoke("ResurrectTimer", 1f);
        else {
            ResurrectUI.SetActive(false);
            transform.position = StartPos;
            resurrectCountDown = 5;
            gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        //플레이어가 플랫폼과 충돌할 때
        if(collision.gameObject.layer.Equals(8)) {
            isJumping = false;
        }
    }
}