﻿using UnityEngine;
using System.Collections;

public class ItemPickup : Interactable {
    public Item item;
    private Effect particle;
    private Rigidbody rigid;

    bool showingMessage = false;

    protected override void Awake() {
        base.Awake();
        rigid = GetComponent<Rigidbody>();
    }

    public void Init(Transform monsterPos) {
        transform.position = monsterPos.position;
        rigid.AddForce(new Vector3(Random.Range(-2, 2), Random.Range(4, 7), Random.Range(-2, 2)), ForceMode.Impulse);

        particle = ParticleController.PlayParticles("ItemIdleParticle", transform);
        StartCoroutine(DisableItemCoroutine());
    }

    public override void Interact() {
        bool wasPickedup = Inventory.Instance.Add(item);
        if(wasPickedup) {
            NotificationManager.Instance.Generate_GetItem(item.name, 1);
            DisableItem();
        }
        else {
            if(!showingMessage) {
                showingMessage = true;
                NotificationManager.Instance.Generate_InventoryIsFull();
                Invoke("SetNotificationInterval", 2f);
            }
        }

    }

    void SetNotificationInterval() {
        showingMessage = false;
    }

    IEnumerator DisableItemCoroutine() {
        yield return new WaitForSeconds(60f);
        DisableItem();
    }

    private void DisableItem() {
        HasInteracted = true;
        particle.Disable();
        gameObject.SetActive(false);
    }
    
}