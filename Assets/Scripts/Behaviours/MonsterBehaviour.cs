using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviour : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] DetectionZone attackTrigger;
    [SerializeField] DetectionZone detectionTrigger;

    private void Start()
    {
        attackTrigger.OnDetection.AddListener(Attack);
        detectionTrigger.OnDetection.AddListener(Follow);
    }

    void Update()
    {
        if (target == null)
        {
            if (detectionTrigger.GetObjectsInArea().Count > 0)
            {
                target = detectionTrigger.GetObjectsInArea()[0];
            }
            return;
        }

        agent.SetDestination(target.transform.position);
    }

    void Attack(GameObject target)
    {
        LifeManager targetLife;

        if (target.TryGetComponent<LifeManager>(out targetLife))
        {
            targetLife.Kill();
        }
    }

    void Follow(GameObject target)
    {
        if (target.CompareTag("Player"))
        {
            this.target = target;
        }
    }
}
