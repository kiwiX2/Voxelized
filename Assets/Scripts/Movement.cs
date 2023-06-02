using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 25f;
    public float sprintSpeed = 35f;
    public float jumpHeight = 2f;
    public int dragOnGround = 2; 
    public int dragInAir = 1;
    bool grounded = false;

    Rigidbody charRb;
    Collider sphereCollider;
    public Transform camera;

    void Start() 
    {
        charRb = GetComponent<Rigidbody>();
        sphereCollider = gameObject.transform.GetChild(0).GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        Vector3 charMovement = (transform.forward * vertical + transform.right * horizontal).normalized;
        charRb.AddForce(charMovement * speed, ForceMode.Force);

        
        if (Input.GetButton("Jump") && grounded) 
        {
            grounded = false;
            charRb.drag = dragInAir;
            charRb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision) 
    {
        grounded = true;
        charRb.drag = dragOnGround;
    }
}
