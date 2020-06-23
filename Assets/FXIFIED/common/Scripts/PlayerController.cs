using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float Speed;

    void Update ()
    {
    float xSpeed = Input.GetAxis("Horizontal");
    float ySpeed = Input.GetAxis("Vertical");

        Rigidbody body = GetComponent<Rigidbody>();
        body.AddTorque(new Vector3(xSpeed, 0, ySpeed) * Speed * Time.deltaTime);

    }
}
