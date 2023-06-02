using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float sprintSpeed = 7f;
    public float jumpHeight = 2f;

    Rigidbody charRb;
    public Transform camera;

    void Start() 
    {
        charRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        Vector3 charMovement = (transform.forward * vertical + transform.right * horizontal).normalized;
        charRb.velocity = charMovement * Time.deltaTime * speed;
    }
}
