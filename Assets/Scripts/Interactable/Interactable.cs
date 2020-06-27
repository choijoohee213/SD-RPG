using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected Transform playerTransform;

    protected bool hasInteracted = false;

    float radius = 1.5f;
    float DistanceFromPlayer => Vector3.Distance(playerTransform.position, transform.position);

    private void OnEnable() {
        hasInteracted = false;
    }

    public virtual void Interact() {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasInteracted) {
            if(DistanceFromPlayer <= radius) {
                Interact();
            }
        }
    }
}
