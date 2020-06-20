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
    public bool AttackEvent { get; set; }
    public bool IsColliderDie { get { return raycastHit.collider != null && raycastHit.collider.GetComponent<CharacterBase>().IsDie; } }

    public event System.Action<float, float> OnHealthChanged;
    public Transform AttackEffectPos;

    [Header("Character Inform")]
    public float MaxHealth;
    public float CurrentHealth;
    public float Damage;

    protected virtual void Awake() {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable() {
        CurrentHealth = MaxHealth;
    }

    public virtual bool CheckRaycastHit(string layerName) {
        return Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), transform.forward, out raycastHit, 2f, 1 << LayerMask.NameToLayer(layerName)) && raycastHit.collider != null;
    }

    public bool AttackToTarget(string layerName) {

        bool isCollider = CheckRaycastHit(layerName);
        if(isCollider && !raycastHit.collider.GetComponent<CharacterBase>().IsDie) {
            if(AttackEvent) {
                AttackEvent = false;
                raycastHit.collider.GetComponent<CharacterBase>().TakeDamage(Damage);
                if(gameObject.layer.Equals(10)) {
                }
            }
        }
        return isCollider;
    }

    public void TakeDamage(float damage) {
        CurrentHealth -= damage;
        if(CurrentHealth < 0) {
            CurrentHealth = 0;
        }

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    private void AttackAnimEvent() {
        AttackEvent = true;
    }

    private void AttackEffect(string particleName) {
        if(raycastHit.collider != null )
            ParticleController.PlayParticles(particleName, AttackEffectPos);
    }

    protected virtual void DieAnimEvent() {
    }
}