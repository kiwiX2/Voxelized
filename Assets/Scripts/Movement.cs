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

    float transformTarget;
    float transformAmount = 0.05f;

    float charRotation;

    Rigidbody charRb;
    Transform charTransform;
    Collider sphereCollider;
    public Transform camera;

    public Transform charBody;
    public Transform charLeftFoot;
    public Transform charRightFoot;
    public Transform charLeftHand;
    public Transform charRightHand;

    Vector3 leftFootOriginalPos;
    Vector3 rightFootOriginalPos;

    void Start() 
    {
        charRb = GetComponent<Rigidbody>();
        charTransform = GetComponent<Transform>();
        sphereCollider = gameObject.transform.GetChild(0).GetComponent<Collider>();

        leftFootOriginalPos = charLeftFoot.transform.localPosition;
        rightFootOriginalPos = charRightFoot.transform.localPosition;
    }

    void FixedUpdate()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        Vector3 charMovement = (transform.forward * vertical + transform.right * horizontal).normalized;
        charRb.AddForce(charMovement * speed, ForceMode.Force);

        AnimateChar(vertical, horizontal);
        
        if (Input.GetButton("Jump") && grounded) 
        {
            grounded = false;
            charRb.drag = dragInAir;
            charRb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }
        
        float mouseDelta = Input.GetAxisRaw("Mouse X");

        if (mouseDelta == 0)
        {
            return;
        }

        charRotation += mouseDelta;
        charTransform.transform.rotation = Quaternion.Euler(0, charRotation, 0);
    }

    void OnCollisionEnter(Collision collision) 
    {
        grounded = true;
        charRb.drag = dragOnGround;
    }

    void AnimateChar(float vertical, float horizontal)
    {
        //rotate body parts toward walking direction
        float leanTargetX = vertical * 10f;
        float leanTargetY = horizontal * 10f;

        if (vertical < 0)
        {
            leanTargetY *= -1f;
        }

        Quaternion footRotation = Quaternion.Euler(0, leanTargetY, 0);
        Quaternion leanTargetRotation = Quaternion.Euler(leanTargetX, leanTargetY, 0);

        RotationLerp(charBody, leanTargetRotation);
        RotationLerp(charLeftHand, leanTargetRotation);
        RotationLerp(charRightHand, leanTargetRotation);

        RotationLerp(charLeftFoot, footRotation);
        RotationLerp(charRightFoot, footRotation);
        
        //hands and feet position back and forth
        transformTarget = Mathf.Sin(Time.time * 8f) * transformAmount;

        Transformationinator(charLeftFoot, 1, horizontal, vertical);
        Transformationinator(charRightFoot, -1, horizontal, vertical);
        Transformationinator(charLeftHand, 1, horizontal, vertical);
        Transformationinator(charRightHand, -1, horizontal, vertical);
    }

    void RotationLerp(Transform rotationObject, Quaternion targetRotation)
    {
        rotationObject.transform.localRotation = Quaternion.Lerp(
            rotationObject.transform.localRotation, 
            targetRotation, 
            0.2f);
    }

    void Transformationinator(Transform transformObject, int transformDirection, float horizontal, float vertical)
    {
        transformObject.transform.localPosition = new Vector3(
            transformDirection * transformTarget * horizontal, 
            0, 
            transformDirection * transformTarget * vertical);
    }
}
