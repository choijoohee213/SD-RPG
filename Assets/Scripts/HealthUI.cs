using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
    private GameObject healthBar;
    public Image healthGauge;

    float visibleTime = 5;
    float lastMadeVisibleTime;
    float currentHP = 1, maxHP = 1, currentFill = 1;

    // Start is called before the first frame update
    void Start() {
       
        healthBar = GameManager.Instance.objectPool.GetObject("HealthBar");
        healthGauge = healthBar.transform.GetChild(0).GetComponent<Image>();
        healthGauge.fillAmount = 1;
        healthBar.SetActive(false);

        GetComponent<FSMBase>().OnHealthChanged += OnHealthChanged;
    }

    void OnHealthChanged(float currentHealth, float maxHealth) {
        currentHP = currentHealth;
        maxHP = maxHealth;
        currentFill = currentHP / maxHP;

        healthBar.SetActive(true);
        lastMadeVisibleTime = Time.time;
    }


    private void Update() {
        if(currentFill != healthGauge.fillAmount) {
            healthGauge.fillAmount = Mathf.Lerp(healthGauge.fillAmount, currentFill, 2f * Time.deltaTime);
        }
        if(healthGauge.fillAmount <= 0.05f)
            healthBar.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        healthBar.transform.position = transform.position + new Vector3(0, 2f, 0);
        healthBar.transform.forward = -GameManager.Instance.cam.transform.forward;
    
        if(Time.time - lastMadeVisibleTime > visibleTime) {
            healthBar.SetActive(false);
        }        
    }
}
