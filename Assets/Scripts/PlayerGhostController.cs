using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float movementSpeed = 2, jumpForce = 10, groundDetectionDistance = 0.1f;
    [SerializeField] Vector3 groundDetectionOffset;

    bool desiredJump = false;
    bool isGrounded = false;

    Vector3 movementDirection = Vector3.zero;

    private void Update()
    {
        desiredJump |= Input.GetKey(KeyCode.Space);
        movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();

        if (isGrounded)
        {
            rb.velocity = movementDirection * movementSpeed;
        }

        if (desiredJump && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        desiredJump = false;
    }

    private bool IsGrounded()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + groundDetectionOffset, Vector3.down, out hit, groundDetectionDistance))
        {
            return true;
        }
        return false;
    }
}