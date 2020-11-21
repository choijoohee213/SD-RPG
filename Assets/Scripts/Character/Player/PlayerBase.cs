using UnityEngine;

public class PlayerBase : CharacterBase {
    public int Level = 1;

    private ExpBar expBar;
    public Vector3 MoveDir { get; set; }
    public Vector3 StartPos { get { return new Vector3(78f, 21.8f, 30f); } }

    private int resurrectCountDown = 5;

    public Animator Anim_levelup;
    public bool IsJumping { get; set; }
    public bool AttackBtnPressed { get; set; }
    private readonly bool attackEvent;

    private Joystick joystick;

    protected override void Awake() {
        base.Awake();
        expBar = GetComponent<ExpBar>();
        joystick = UIManager.Instance.joystick;
        UIManager.Instance.playerLevelText.text = "Lv. " + Level.ToString();
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

    public void Heal(float healthGain) {
        CurrentHealth += healthGain;
        if(CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;

        healthBar.OnHealthChanged(CurrentHealth, MaxHealth);
    }

    public void IncreaseExp(float ExpGained) {
        float resultExp = CurrentExp + ExpGained;
        if(resultExp >= MaxExp) {
            while(resultExp >= MaxExp) {
                resultExp -= MaxExp;
                LevelUp();
            }
            CurrentExp += resultExp;
        }
        else {
            CurrentExp = resultExp;
        }
        expBar.OnExpChanged(CurrentExp, MaxExp);

        NotificationManager.Instance.Generate_GetExp(ExpGained);
    }

    private void LevelUp() {
        Level++;

        MaxHealth += 50;
        CurrentHealth = MaxHealth;

        MaxExp += 20;
        CurrentExp = 0;

        MinimalDamage += 1;

        healthBar.OnHealthChanged(CurrentHealth, MaxHealth);
        expBar.OnExpChanged(CurrentExp, MaxExp);
        UIManager.Instance.playerLevelText.text = "Lv. " + Level.ToString();

        Anim_levelup.SetTrigger("levelup");
        ParticleController.PlayParticles("PlayerLevelUpParticle", transform);
    }

    public void PlayerJumpBtn() {
        if(!IsJumping) {
            Rigid.AddForce(Vector3.up * 70, ForceMode.Impulse);
            SoundManager.Instance.playAudio("Jump");
            IsJumping = true;
        }
    }

    public void PlayerAttackBtn(bool pressed) {
        AttackBtnPressed = pressed;
    }

    private void PlayerAttackSound() {
        SoundManager.Instance.playAudio("PlayerAttack");
    }

    protected override void DieAnimEvent() {
        SoundManager.Instance.playAudio("PlayerDie");
        base.DieAnimEvent();
        if(BossQuest.Instance.OnFighting) {
            BossQuest.Instance.ExitCastle();
        }
        UIManager.Instance.ResurrectUI.SetActive(true);
        ResurrectTimer();
    }

    private void ResurrectTimer() {
        UIManager.Instance.countDownText.text = resurrectCountDown.ToString();
        if(resurrectCountDown-- != 0)
            Invoke("ResurrectTimer", 1f);
        else {
            resurrectCountDown = 5;
            UIManager.Instance.ResurrectUI.SetActive(false);
            gameObject.SetActive(true);
            ParticleController.PlayParticles("PlayerResurrectParticle", transform);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        //플레이어가 플랫폼과 충돌할 때
        if(collision.gameObject.layer.Equals(13)) {
            IsJumping = false;
        }
    }
}