using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBase : CharacterBase {

    public Vector3 limitRange_Min, limitRange_Max;

    public override bool CheckRaycastHit(string layerName) {
        return (Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), transform.forward, out raycastHit, 1.5f, 1 << LayerMask.NameToLayer(layerName))
                || Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), transform.right, 1.5f, 1 << LayerMask.NameToLayer(layerName))
                || Physics.Raycast(transform.position + new Vector3(0f, 0.2f, 0), -transform.right, 1.5f, 1 << LayerMask.NameToLayer(layerName))) && raycastHit.collider != null;
    }

    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);

        //데미지 텍스트 프리팹으로 생성
        Text damageText = GameManager.Instance.objectPool.GetObject("DamageText").GetComponent<Text>();
        damageText.text = damage.ToString();
        damageText.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 3f, 0));
    }


    protected override void DieAnimEvent() {
        GetComponent<HealthUI>().healthBar.SetActive(false);
        gameObject.SetActive(false);
        Invoke("Resurrect", 3);
    }

    private void Resurrect() {
        transform.position = new Vector3(Random.Range(limitRange_Min.x, limitRange_Max.x), transform.position.y, Random.Range(limitRange_Min.z, limitRange_Max.z));
        gameObject.SetActive(true);
    }

}