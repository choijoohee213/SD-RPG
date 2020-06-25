using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {

    public ParticleSystem[] particles;
    
    public float lifeTime;

    private void OnEnable() {
        Invoke("Disable", lifeTime);

    }

    void Disable() {
        gameObject.SetActive(false);
    }
   
}
