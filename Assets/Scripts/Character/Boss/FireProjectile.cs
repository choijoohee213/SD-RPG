using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour {
    Effect effect;
    Rigidbody rigid;
    Vector3 v = new Vector3(0f, 5f, 0f);

    private void Awake() {
        effect = GetComponent<Effect>();
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable() {
        rigid.velocity = v;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer.Equals(9)) {
            Debug.Log("충돌!");
            //effect.Disable();
        }
    }
}
