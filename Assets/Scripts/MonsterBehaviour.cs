using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviour : MonoBehaviour
{
    [SerializeField] GameObject player;
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
        if (player == null)
        {
            return;
        }

        agent.SetDestination(player.transform.position);
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
            player = target;
        }
    }
}
