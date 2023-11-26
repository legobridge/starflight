using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battleship : MonoBehaviour
{ 
    public float playerSpeed = 20f;
    private Rigidbody rb;
    private Vector3 shipDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.forward * playerSpeed;
    }

}
