using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostController : MonoBehaviour
{
    [SerializeField] LifeManager lifeManager;

    public List<MovementCommand> commands;
    int commandIndex = 0;

    [SerializeField] Rigidbody rb;
    [SerializeField] float movementSpeed = 2, jumpForce = 10, groundDetectionDistance = 0.1f;
    [SerializeField] Vector3 groundDetectionOffset;

    bool desiredJump = false;
    bool isGrounded = false;

    Vector3 movementDirection = Vector3.zero;

    private void Start()
    {
        commandIndex = 0;
    }

    private void Update()
    {
        if (commandIndex < commands.Count)
        {
            desiredJump |= commands[commandIndex].desiredJump;
            movementDirection = commands[commandIndex].movementDirection;

            Debug.Log(desiredJump + ", " +  movementDirection); 
        }
        else
        {
            desiredJump = false;
            movementDirection = Vector3.zero;
        }

        commandIndex++;
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