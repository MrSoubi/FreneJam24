using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalBehaviour : MonoBehaviour
{
    [SerializeField] DetectionZone zone;

    public UnityEvent OnActivation;

    private void Start()
    {
        zone.OnDetection.AddListener(HandleActivation);
    }

    void HandleActivation(GameObject activator)
    {
        OnActivation.Invoke();
    }
}
