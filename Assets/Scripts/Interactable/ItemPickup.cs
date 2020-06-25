using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemPickup : Interactable
{
    public Item item;

    private Rigidbody rigid;

    private void Awake() {
        rigid = GetComponent<Rigidbody>();
    }

    public void Init(Transform monsterPos, Transform _playerTransform) {
        transform.position = monsterPos.position;
        playerTransform = _playerTransform;
        rigid.AddForce(new Vector3(Random.Range(-2, 2), Random.Range(4, 7), Random.Range(-2, 2)), ForceMode.Impulse);
    }

    public override void Interact() {
        bool wasPickedup = Inventory.Instance.Add(item);
        if(wasPickedup) {
            hasInteracted = true;
            gameObject.SetActive(false);
        }
    }

}
