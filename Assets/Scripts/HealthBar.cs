using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;

    [SerializeField]
    GameObject healthBarPrefab;

    Transform cam, healthBar;
    Image gauge;
    //public Transform targetMonster;

    void Start() {
        cam = Camera.main.transform;
        healthBar = GetComponent<Transform>();
        healthBar = Instantiate(healthBarPrefab, canvas.transform).transform;
        gauge = healthBar.GetChild(0).GetComponent<Image>();
        
    }

    private void LateUpdate() {
        healthBar.position = transform.position + new Vector3(0, 2f, 0);
        healthBar.forward = -cam.forward;
    }
}
