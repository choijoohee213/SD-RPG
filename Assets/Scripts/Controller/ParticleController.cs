using UnityEngine;

#pragma warning disable CS0618

public class ParticleController : MonoBehaviour {

    public static Effect PlayParticles(string name, Transform effectPos) {
        Effect effect = GameManager.Instance.objectPool.GetObject(name).GetComponent<Effect>();
        effect.Init(effectPos);

        ParticleSystem[] particles = effect.particles;
        for(int i = 0; i < particles.Length; i++)
            particles[i].Play();

        return effect;
    }
}