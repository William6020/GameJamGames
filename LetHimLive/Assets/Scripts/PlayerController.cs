using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform cameraTransform;
    public float maxChargeTime = 2f;
    public float maxForce = 20f;
    public float upwardForce = 5f;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;

    private float chargeTime = 0f;
    private bool isCharging = false;
    private bool isGrounded = false;

    void Update()
    {
        CheckGrounded();

        if (isGrounded && Input.GetKey(KeyCode.W))
        {
            isCharging = true;
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(chargeTime, 0f, maxChargeTime);
        }

        if (isGrounded && Input.GetKeyUp(KeyCode.W) && isCharging)
        {
            Launch();
            isCharging = false;
            chargeTime = 0f;
        }
    }

    void Launch()
    {
        float chargeRatio = chargeTime / maxChargeTime;
        float force = chargeRatio * maxForce;

        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 launchDirection = cameraForward * force + Vector3.up * upwardForce;

        rb.AddForce(launchDirection, ForceMode.Impulse);
    }

    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    void OnCollisionStay(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }
}
