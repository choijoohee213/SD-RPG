using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    float radius = 2f;
    float DistanceFromPlayer => Vector3.Distance(player.position, transform.position);
    public Transform player;

    private void Start() {
        player = PlayerBase.instance.gameObject.transform;
    }

    public virtual void Interact() {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DistanceFromPlayer <= radius) {
            Interact();
        }
    }
}
