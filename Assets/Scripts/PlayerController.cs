using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float movementSpeed = 2, jumpForce = 10, groundDetectionDistance = 0.1f;
    [SerializeField] Vector3 groundDetectionOffset;
    [SerializeField] InputManager inputManager;

    bool desiredJump = false;
    bool isGrounded = false;

    Vector3 movementDirection = Vector3.zero;

    private void Update()
    {
        desiredJump |= inputManager.desiredJump;
        movementDirection = inputManager.movementDirection;
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