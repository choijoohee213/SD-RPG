using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    float lifeTime = 2f;

    private void OnEnable() {
        Invoke("Disable", lifeTime);

    }

    void Disable() {
        gameObject.SetActive(false);
    }
}
