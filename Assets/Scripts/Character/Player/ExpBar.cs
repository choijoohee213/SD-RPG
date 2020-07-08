using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour {
    public GameObject expBar;
    private Image expGauge;
    private Text expText;

    private float gaugeMoveSpeed = 2f;
    private float currentEXP, maxEXP, currentEXPFill;

    private void Awake() {
        expGauge = expBar.transform.GetChild(0).GetComponent<Image>();
        expText = expBar.transform.GetChild(1).GetComponent<Text>();

        maxEXP = GameManager.Instance.player.MaxExp;
    }

    private void Update() {
        if(currentEXPFill != expGauge.fillAmount)
            expGauge.fillAmount = Mathf.Lerp(expGauge.fillAmount, currentEXPFill, gaugeMoveSpeed * Time.deltaTime);

        expText.text = currentEXP.ToString() + " / " + maxEXP.ToString();
    }

    public void OnExpChanged(float currentExp, float maxExp) {
        currentEXP = currentExp;
        maxEXP = maxExp;
        currentEXPFill = currentEXP / maxEXP;
    }
}