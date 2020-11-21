using UnityEngine;

public abstract class CharacterBase : MonoBehaviour {
    public Animator Anim { get; set; }
    public Rigidbody Rigid { get; set; }
    protected HealthBar healthBar;

    protected RaycastHit raycastHit;

    public bool IsDie { get => CurrentHealth <= 0; }
    public bool AttackStart { get; set; }
    public bool IsColliderDie { get { return raycastHit.collider != null && raycastHit.collider.GetComponent<CharacterBase>().IsDie; } }

    public Transform AttackEffectPos;

    [Header("Character Inform")]
    public float MaxHealth;

    public float CurrentHealth;

    public float MaxExp;
    public float CurrentExp { get; set; }

    public int MinimalDamage;
    public float Damage { get => Random.Range(MinimalDamage, MinimalDamage + 3); }

    protected virtual void Awake() {
        Anim = GetComponent<Animator>();
        Rigid = GetComponent<Rigidbody>();
        healthBar = GetComponent<HealthBar>();
    }

    protected virtual void OnEnable() {
        CurrentHealth = MaxHealth;
    }

    public virtual bool CheckRaycastHit(string layerName) {
        return Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), transform.forward, out raycastHit, 2f, 1 << LayerMask.NameToLayer(layerName)) && raycastHit.collider != null;
    }

    public virtual bool AttackToTarget(string layerName) {
        bool isCollider = CheckRaycastHit(layerName);
        if(isCollider) {
            CharacterBase character = raycastHit.collider.GetComponent<CharacterBase>();
            if(!character.IsDie && AttackStart) {
                AttackStart = false;
                character.TakeDamage(Damage);
            }
        }
        return isCollider;
    }

    public virtual void TakeDamage(float damage) {
        CurrentHealth -= damage;
        if(CurrentHealth < 0) {
            CurrentHealth = 0;
        }

        //체력바 게이지 감소
        healthBar.OnHealthChanged(CurrentHealth, MaxHealth);
    }

    protected void AttackAnimEvent() {
        AttackStart = true;
    }

    private void AttackEffect(string particleName) {
        if(raycastHit.collider != null) {
            ParticleController.PlayParticles(particleName, AttackEffectPos);
            SoundManager.Instance.playAudio("Hit");
        }
    }

    protected virtual void DieAnimEvent() {
        ParticleController.PlayParticles("DieParticle", transform);
        gameObject.SetActive(false);
    }
}