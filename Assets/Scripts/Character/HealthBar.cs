using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    private CharacterBase characterBase;
    private CharacterFSM characterFSM;
    public GameObject healthBarObj;
    private Image healthGauge;
    private Text healthText;

    private float visibleTime = 5f, lastMadeVisibleTime, gaugeMoveSpeed = 2f;
    private float currentHP, maxHP, currentHPFill;

    private void Awake() {
        //몬스터
        if(gameObject.layer.Equals(10)) {
            healthBarObj = GameManager.Instance.objectPool.GetObject("HealthBar");
            healthBarObj.SetActive(false);
        }
        characterBase = GetComponent<CharacterBase>();
        characterFSM = GetComponent<CharacterFSM>();
        healthGauge = healthBarObj.transform.GetChild(0).GetComponent<Image>();
        healthText = healthBarObj.transform.GetChild(1).GetComponent<Text>();
    }

    private void OnEnable() {
        if(currentHP == 0) {
            maxHP = characterBase.MaxHealth;
            currentHP = maxHP;
            currentHPFill = 1;
            healthGauge.fillAmount = 1;
        }
    }

    private void Update() {
        if(currentHPFill != healthGauge.fillAmount)
            healthGauge.fillAmount = Mathf.Lerp(healthGauge.fillAmount, currentHPFill, gaugeMoveSpeed * Time.deltaTime);

        healthText.text = currentHP.ToString() + " / " + maxHP.ToString();
    }

    private void LateUpdate() {
        //몬스터 체력바
        if(gameObject.layer.Equals(10)) {
            healthBarObj.transform.position = transform.position + new Vector3(0, 2f, 0);
            healthBarObj.transform.forward = GameManager.Instance.Cam.transform.forward;

            if(Time.time - lastMadeVisibleTime > visibleTime) {
                healthBarObj.SetActive(false);
            }
        }
    }

    public void OnHealthChanged(float currentHealth, float maxHealth) {
        currentHP = currentHealth;
        maxHP = maxHealth;
        currentHPFill = currentHP / maxHP;

        if(gameObject.layer.Equals(10)) {
            healthBarObj.SetActive(true);
            lastMadeVisibleTime = Time.time;
        }

        if(currentHP <= 0)
            characterFSM.SetState(CharacterState.Die);
    }
}