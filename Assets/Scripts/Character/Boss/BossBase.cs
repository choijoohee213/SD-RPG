using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBase : CharacterBase {
    private Vector3 BossPos = new Vector3(-29.3f, 21.5f, 248.7f);
    private CameraController cameraController;
    private float attackJumpRange = 9f;

    public int attackFireCount { get; set; }

    protected override void Awake() {
        base.Awake();
        cameraController = GameManager.Instance.Cam.GetComponent<CameraController>();
    }

    public override bool AttackToTarget(string layerName) {
        PlayerBase player = GameManager.Instance.player;
        Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        bool withInRange = Vector3.Distance(targetPos, transform.position) <= attackJumpRange;
        if(withInRange && !player.IsDie && AttackStart) {
            AttackStart = false;
            player.TakeDamage(Damage);
        }
        return withInRange;
    }

    public void CreateFireBall() {
        ParticleController.PlayParticles("BossAttackFireParticle", GameManager.Instance.player.transform);
        if(attackFireCount == 3)
            AttackStart = false;
    }

    private void AttackJumpAnimEvent() {
        AttackToTarget("Player");
        ParticleController.PlayParticles("BossAttackJumpParticle", transform);
        cameraController.CameraShake(0.5f, 0.4f);
    }

    private void AttackFireAnimEvent() {
        attackFireCount++;
        CreateFireBall();
    }

    protected override void DieAnimEvent() {
        SceneManager.LoadScene(1);
    }
}