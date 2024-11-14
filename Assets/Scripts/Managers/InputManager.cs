using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] CommandRecorder commandRecorder;

    public bool desiredJump = false;
    public Vector3 movementDirection = Vector3.zero;

    private void Update()
    {
        desiredJump = Input.GetKey(KeyCode.Space);
        movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

        MovementCommand command = new MovementCommand();

        command.movementDirection = movementDirection;
        command.desiredJump = desiredJump;

        commandRecorder.RecordStep(command);
    }
}
