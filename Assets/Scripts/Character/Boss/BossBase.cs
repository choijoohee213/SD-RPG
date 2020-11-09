using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : CharacterBase
{    
    Vector3 BossPos = new Vector3(-29.3f, 21.5f, 248.7f);
    CameraController cameraController;
    float attackJumpRange = 9f;

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

    public void CreateFireBall(Transform pos) {
        ParticleController.PlayParticles("BossAttackFireParticle", pos);

    }

    protected override void AttackAnimEvent() {
        base.AttackAnimEvent();
        AttackToTarget("Player");
        ParticleController.PlayParticles("BossAttackJumpParticle", transform);
        cameraController.CameraShake(0.5f, 0.4f);
    }

    //지워
    private void OnDrawGizmos() {
        //Gizmos.DrawWireSphere(transform.position + transform.forward * 0, attackJumpRange);
    }
}
