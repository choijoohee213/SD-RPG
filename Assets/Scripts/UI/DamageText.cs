using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    private readonly float lifeTime = 1.3f;
    private float startTime;
    Text damageText;
    Color alpha;

    public void Init(GameObject _damageText, Vector3 characterPos, float damage) {
        damageText = _damageText.GetComponent<Text>();
        damageText.text = damage.ToString();
        damageText.color = new Color(damageText.color.r, damageText.color.g, damageText.color.b, 1);
        damageText.transform.position = Camera.main.WorldToScreenPoint(characterPos + new Vector3(0, 2.5f, 0));
        
        alpha = damageText.color;
        
        startTime = Time.time;
        
        StartCoroutine(MoveText());
    }

    IEnumerator MoveText() {
        while(Time.time - startTime <= lifeTime) {
            damageText.transform.Translate(Vector3.up * 40f * Time.deltaTime);
            alpha.a = Mathf.Lerp(alpha.a, 0f, 0.7f * Time.deltaTime);
            damageText.color = alpha;
            yield return null;
        }
        gameObject.SetActive(false);
    }

}
