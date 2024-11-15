using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float horizontalSensibility = 3, verticalSensibility = 2;

    Vector3 lastMousePosition = Vector3.zero;

    void Update()
    {
        Vector3 delta = lastMousePosition - Input.mousePosition;
        Vector3 rotation = new(delta.y * horizontalSensibility, delta.x * verticalSensibility, 0);
        transform.Rotate(rotation * Time.deltaTime);
        lastMousePosition = Input.mousePosition;
    }
}
