using UnityEngine;

public class ItemPickup : Interactable {
    public Item item;
    private Effect particle;
    private Rigidbody rigid;

    protected override void Awake() {
        base.Awake();
        rigid = GetComponent<Rigidbody>();
    }

    public void Init(Transform monsterPos) {
        transform.position = monsterPos.position;
        rigid.AddForce(new Vector3(Random.Range(-2, 2), Random.Range(4, 7), Random.Range(-2, 2)), ForceMode.Impulse);

        particle = ParticleController.PlayParticles("ItemIdleParticle", transform);
        Invoke("DisableItem", 60f);
    }

    public override void Interact() {
        bool wasPickedup = Inventory.Instance.Add(item);
        if(wasPickedup) {
            DisableItem();
        }
    }

    private void DisableItem() {
        HasInteracted = true;
        particle.Disable();
        gameObject.SetActive(false);
    }
}