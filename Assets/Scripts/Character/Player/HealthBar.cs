using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public GameObject healthBar, expBar;
    private Image healthGauge, expGauge;
    private Text healthText, expText;

    private float visibleTime = 5, lastMadeVisibleTime, decreaseSpeed = 2f;
    private float currentHP, maxHP, currentHPFill;

    private void Awake() {
        if(gameObject.layer.Equals(10)) {  //몬스터
            healthBar = GameManager.Instance.objectPool.GetObject("HealthBar");
            healthBar.SetActive(false);
        }
        else {
            expGauge = expBar.transform.GetChild(0).GetComponent<Image>();
            expText = expBar.transform.GetChild(0).GetComponent<Text>();
        }
        healthGauge = healthBar.transform.GetChild(0).GetComponent<Image>();
        healthText = healthBar.transform.GetChild(1).GetComponent<Text>();

        GetComponent<CharacterBase>().OnHealthChanged += OnHealthChanged;
    }

    private void OnEnable() {
        if(currentHP == 0) {
            maxHP = GetComponent<CharacterBase>().MaxHealth;
            currentHP = maxHP;
            currentHPFill = 1;
            healthGauge.fillAmount = 1;
        }
    }

    private void OnHealthChanged(float currentHealth, float maxHealth) {
        currentHP = currentHealth;
        maxHP = maxHealth;
        currentHPFill = currentHP / maxHP;

        if(gameObject.layer.Equals(10)) {
            healthBar.SetActive(true);
            lastMadeVisibleTime = Time.time;
        }

    }

    private void Update() {
        if(currentHPFill != healthGauge.fillAmount) {
            healthGauge.fillAmount = Mathf.Lerp(healthGauge.fillAmount, currentHPFill, decreaseSpeed * Time.deltaTime);
        }

        healthText.text = currentHP.ToString() + " / " + maxHP.ToString();
    }

    private void LateUpdate() {
        if(gameObject.layer.Equals(10)) {
            healthBar.transform.position = transform.position + new Vector3(0, 2f, 0);
            healthBar.transform.forward = GameManager.Instance.Cam.transform.forward;

            if(Time.time - lastMadeVisibleTime > visibleTime) {
                healthBar.SetActive(false);
            }
        }
    }
}