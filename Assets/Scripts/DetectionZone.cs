using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class DetectionZone : MonoBehaviour
{
    public UnityEvent<GameObject> OnDetection;

    private void OnTriggerEnter(Collider other)
    {
        OnDetection.Invoke(other.gameObject);
    }
}
