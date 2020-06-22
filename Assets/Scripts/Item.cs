using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string ItemName;
    public string SourceMonsterName;

    private Rigidbody rigid;

    private void Awake() {
        rigid = GetComponent<Rigidbody>();    
    }

    public void Init(Transform monsterPos) {
        transform.position = monsterPos.position;
        rigid.AddForce(new Vector3(Random.Range(-2,2), Random.Range(4,7), Random.Range(-2,2)) , ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision != null && collision.gameObject.layer.Equals(9)) {
            gameObject.SetActive(false);
        }
    }
}
