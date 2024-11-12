using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandRecorder : MonoBehaviour
{
    public List<MovementCommand> movementCommands;

    public void StartRecording()
    {
        movementCommands = new List<MovementCommand>();
    }

    public void RecordStep(MovementCommand command)
    {
        if (movementCommands == null)
        {
            StartRecording();
        }
        movementCommands.Add(command);
    }
}

[System.Serializable]
public struct MovementCommand
{
    public Vector3 movementDirection;
    public bool desiredJump;
}