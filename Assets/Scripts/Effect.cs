using UnityEngine;

public class Effect : MonoBehaviour {
    public ParticleSystem[] particles;
    public Vector3 plusPos;
    public Transform StartTransform { get; set; }
    public bool updatingPos;
    public float lifeTime;

    public void Init(Transform effectPos) {
        Invoke("Disable", lifeTime);
        transform.position = effectPos.position + plusPos;
        StartTransform = effectPos;
    }

    private void Update() {
        if(updatingPos)
            transform.position = new Vector3(StartTransform.position.x, transform.position.y, StartTransform.position.z);
    }

    public void Disable() {
        gameObject.SetActive(false);
    }
}