using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonBehaviour : MonoBehaviour
{
    public UnityEvent OnActivation;

    private void OnTriggerEnter(Collider other)
    {
        OnActivation.Invoke();
    }
}
