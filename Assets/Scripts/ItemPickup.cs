using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemPickup : MonoBehaviour
{
    public Item item;

    private Rigidbody rigid;

    private void Awake() {
        rigid = GetComponent<Rigidbody>();
    }

    public void Init(Transform monsterPos) {
        transform.position = monsterPos.position;
        rigid.AddForce(new Vector3(Random.Range(-2, 2), Random.Range(4, 7), Random.Range(-2, 2)), ForceMode.Impulse);
    }

    

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.layer.Equals(9)) {
            bool wasPickedup = Inventory.Instance.Add(item);
            if(wasPickedup) gameObject.SetActive(false);
            else {
                Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Item"), true);
            }
        }
    }
}
