using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable CS0618

public class ParticleController
{
    public static void ModifyParticleAwake(ParticleSystem particle, bool awakeOn) {
        particle.playOnAwake = awakeOn;
    }

    public static void ModifyParticlesAwake(ParticleSystem[] particles, bool awakeOn) {
        for(int i = 0; i < particles.Length; i++)
            particles[i].playOnAwake = awakeOn;
    }


}
