using UnityEngine;

public class CharacterBase : MonoBehaviour {
    [SerializeField]
    PlayerBase player;

    private Animator anim;
    public Animator Anim { get => anim; }

    private Rigidbody rigid;
    public Rigidbody Rigid { get => rigid; }

    protected RaycastHit raycastHit;

    public bool IsDie { get => CurrentHealth <= 0; }
    public bool AttackStart { get; set; }
    public bool IsColliderDie { get { return raycastHit.collider != null && raycastHit.collider.GetComponent<CharacterBase>().IsDie; } }

    public event System.Action<float, float> OnHealthChanged;
    public Transform AttackEffectPos;

    [Header("Character Inform")]
    public float MaxHealth;
    public float CurrentHealth;
    
    [SerializeField]
    private int MinimalDamage;
    public float Damage { get => Random.Range(MinimalDamage, MinimalDamage + 3); } 

    protected virtual void Awake() {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    protected virtual void OnEnable() {
        CurrentHealth = MaxHealth;
    }

    public virtual bool CheckRaycastHit(string layerName) {
        return Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), transform.forward, out raycastHit, 2f, 1 << LayerMask.NameToLayer(layerName)) && raycastHit.collider != null;
    }


    public bool AttackToTarget(string layerName) {

        bool isCollider = CheckRaycastHit(layerName);
        if(isCollider && !raycastHit.collider.GetComponent<CharacterBase>().IsDie && AttackStart) {
            AttackStart = false;
            raycastHit.collider.GetComponent<CharacterBase>().TakeDamage(Damage);
        }
        return isCollider;
    }

    public virtual void TakeDamage(float damage) {
        CurrentHealth -= damage;
        if(CurrentHealth < 0) {
            CurrentHealth = 0;
        }

        //체력바 게이지 감소
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    private void AttackAnimEvent() {
        AttackStart = true;
    }

    private void AttackEffect(string particleName) {
        if(raycastHit.collider != null )
            ParticleController.PlayParticles(particleName, AttackEffectPos);
    }

    protected virtual void DieAnimEvent() {
    }
}