using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {

    public ParticleSystem[] particles;
    
    [SerializeField]
    private float lifeTime;

    private void OnEnable() {
        Invoke("Disable", lifeTime);

    }

    void Disable() {
        gameObject.SetActive(false);
    }
   
}
