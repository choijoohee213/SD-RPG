using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : CharacterBase {
    public int Level = 1;

    private ExpBar expBar;
    public Vector3 MoveDir { get; set; }
    public Vector3 StartPos { get { return new Vector3(74, 21.79191f, 44); } }

    private int resurrectCountDown = 5;

    public bool IsJumping {get; set;}
    public bool AttackBtnPressed { get; set; }
    private readonly bool attackEvent;

    [Header("UI")]
    public Joystick joystick;
    public GameObject resurrectUI;
    public Text countDownText;


    protected override void Awake() {
        base.Awake();
        expBar = GetComponent<ExpBar>();
    }

    protected override void OnEnable() {
        base.OnEnable();
        transform.position = StartPos;
        transform.rotation = Quaternion.identity;
    }

    private void FixedUpdate() {
        if(GetComponent<CharacterFSM>().state != CharacterState.Attack && !Joystick.IsPointerUp)
            MoveDir = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
    }
    
    public bool PlayerInMonsterRange(Vector3 minRange, Vector3 maxRange) {
        return transform.position.x < maxRange.x && transform.position.x > minRange.x && transform.position.z < maxRange.z && transform.position.z > minRange.z;
    }

    public void IncreaseExp(float monsterExp) {
        CurrentExp += monsterExp;
        expBar.OnExpChanged(CurrentExp, MaxExp);

        if(CurrentExp.Equals(MaxExp))
            LevelUp();
    }

    private void LevelUp() {
        Level++;

        MaxHealth += 50;
        CurrentHealth = MaxHealth;

        MaxExp += 100;
        CurrentExp = 0;

        MinimalDamage += 1;

        healthBar.OnHealthChanged(CurrentHealth, MaxHealth);
        expBar.OnExpChanged(CurrentExp, MaxExp);

        ParticleController.PlayParticles("PlayerLevelUpParticle", transform);
    }


    public void PlayerJumpBtn() {
        if(!IsJumping) {
            Rigid.AddForce(Vector3.up * 80, ForceMode.Impulse);
            IsJumping = true;
        }
    }

    public void PlayerAttackBtn(bool pressed) {
        AttackBtnPressed = pressed;
    }

    
    protected override void DieAnimEvent() {
        base.DieAnimEvent();
        resurrectUI.SetActive(true);
        ResurrectTimer();
    }

    private void ResurrectTimer() {
        countDownText.text = resurrectCountDown.ToString();
        if(resurrectCountDown-- != 0)
            Invoke("ResurrectTimer", 1f);
        else {
            resurrectCountDown = 5;
            resurrectUI.SetActive(false);
            gameObject.SetActive(true);
            ParticleController.PlayParticles("PlayerResurrectParticle", transform);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        //플레이어가 플랫폼과 충돌할 때
        if(collision.gameObject.layer.Equals(8)) {
            IsJumping = false;
        }
    }
}