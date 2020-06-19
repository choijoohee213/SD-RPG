using System;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0618

[Serializable]
public class Particle {
    public string effectName;    
    public ParticleSystem[] particleSystems;
}

public class ParticleController : MonoBehaviour {
    public Particle[] particles;

    public void ModifyParticlesAwake(string name, bool awakeOn) {
        ParticleSystem[] effect;
        int index = 0;
        for(int i = 0; i < particles.Length; i++) {
            if(particles[i].effectName.Equals(name)) {
                index = i;
                break;
            }
        }
        effect = particles[index].particleSystems;

        for(int i = 0; i < effect.Length; i++)
            effect[i].playOnAwake = awakeOn;
    }
}