using UnityEngine;

public class FireProjectile : MonoBehaviour {
    private Effect effect;
    private Rigidbody rigid;
    private PlayerBase player;

    private Vector3 v = new Vector3(0f, 5f, 0f);
    private readonly float fireDamage = 15f;

    private void Awake() {
        effect = GetComponent<Effect>();
        rigid = GetComponent<Rigidbody>();
        player = GameManager.Instance.player;
    }

    private void OnEnable() {
        rigid.velocity = v;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer.Equals(9)) {
            player.TakeDamage(fireDamage);
        }
    }
}