using UnityEngine;

public class Interactable : MonoBehaviour {
    protected Transform playerTransform;

    public bool HasInteracted { get; set; }

    protected float radius = 1.5f;
    private float DistanceFromPlayer => Vector3.Distance(playerTransform.position, transform.position);

    protected virtual void Awake() {
        playerTransform = GameManager.Instance.player.transform;
        HasInteracted = false;
    }

    private void OnEnable() {
        HasInteracted = false;
    }

    public virtual void Interact() {
    }

    public virtual void NonInteract() {
    }

    // Update is called once per frame
    private void Update() {
        if(!HasInteracted) {
            if(DistanceFromPlayer <= radius) {
                Interact();
            }
        }
        else {
            if(DistanceFromPlayer > radius) {
                NonInteract();
            }
        }
    }
}