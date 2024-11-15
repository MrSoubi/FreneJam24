using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathZoneBehaviour : MonoBehaviour
{
    public UnityEvent OnPlayerEnteredDeathZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            OnPlayerEnteredDeathZone.Invoke();
        }
    }
}
