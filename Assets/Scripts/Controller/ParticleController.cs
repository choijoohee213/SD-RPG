using System;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0618

public class ParticleController : MonoBehaviour {
    public static void PlayParticles(string name, Transform effectPos) {
        GameObject obj = GameManager.Instance.objectPool.GetObject(name);
        obj.transform.position = effectPos.position;
        ParticleSystem[] effects = obj.GetComponent<Effect>().particles;
        for(int i = 0; i < effects.Length; i++) {
            effects[i].Play();
        }
    }

}