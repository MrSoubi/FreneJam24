using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatformBehaviour : MonoBehaviour
{
    [SerializeField] Transform startPosition, endPosition;
    [SerializeField] GameObject platform;

    private void Start()
    {
        platform.transform.position = startPosition.position;
    }

    public void Activate()
    {

        platform.transform.DOMove(endPosition.position, 2);
    }

    public void Reset()
    {
        platform.transform.position = startPosition.position;
    }
}