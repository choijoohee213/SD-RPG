using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : CharacterBase {
    public static PlayerBase instance;
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
    public bool IsAttack { get => isAttack; }
    public bool IsJumping { get => isJumping; }

    protected override void OnEnable() {
        base.OnEnable();
        transform.position = StartPos;
        transform.rotation = Quaternion.identity;
    }

    private void FixedUpdate() {
        if(GetComponent<CharacterFSM>().state != CharacterState.Attack && !Joystick.IsPointerUp) 
            movePos = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
    }
    
    public bool PlayerInMonsterRange(Vector3 minRange, Vector3 maxRange) {
        return transform.position.x < maxRange.x && transform.position.x > minRange.x && transform.position.z < maxRange.z && transform.position.z > minRange.z;
    }

    public void PlayerJumpBtn() {
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
            resurrectCountDown = 5;
            ResurrectUI.SetActive(false);
            gameObject.SetActive(true);
            ParticleController.PlayParticles("PlayerResurrectParticle", transform);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        //플레이어가 플랫폼과 충돌할 때
        if(collision.gameObject.layer.Equals(8)) {
            isJumping = false;
        }
    }
}