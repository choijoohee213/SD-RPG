using UnityEngine;

public class MonsterBase : CharacterBase {


    public override bool CheckRaycastHit(string layerName) {
        return (Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), transform.forward, out raycastHit, 1.5f, 1 << LayerMask.NameToLayer(layerName))
                || Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), transform.right, 1.5f, 1 << LayerMask.NameToLayer(layerName))
                || Physics.Raycast(transform.position + new Vector3(0f, 0.2f, 0), -transform.right, 1.5f, 1 << LayerMask.NameToLayer(layerName))) && raycastHit.collider != null;
    }

    protected override void DieAnimEvent() {
        GetComponent<HealthUI>().healthBar.SetActive(false);
        gameObject.SetActive(false);
        Invoke("Resurrect", 3);
    }

    private void Resurrect() {
        gameObject.SetActive(true);
    }
}