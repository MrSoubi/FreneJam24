using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<MovingPlatformBehaviour> movingPlatforms;

    List<List<MovementCommand>> movementCommandsList = new List<List<MovementCommand>>();

    [SerializeField] int nextLevelIndex;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject ghostPlayerPrefab;
    [SerializeField] GameObject monsterPrefab;
    [SerializeField] Transform playerSpawn;
    [SerializeField] List<Transform> monsterSpawns;
    [SerializeField] List<DeathZoneBehaviour> deathZones;
    [SerializeField] GoalBehaviour goal;

    CommandRecorder commandRecorder;

    List<GameObject> entities = new List<GameObject>();


    private void Start()
    {
        goal.OnActivation.AddListener(OnGoalReached);

        foreach(DeathZoneBehaviour zoneBehaviour in deathZones)
        {
            zoneBehaviour.OnPlayerEnteredDeathZone.AddListener(RetryRound);
        }

        BeginRound();
    }

    void BeginRound()
    {
        ResetPlatforms();
        SpawnMonster();
        SpawnPlayerClones();
        SpawnPlayer();
    }

    private void ResetPlatforms()
    {
        foreach (MovingPlatformBehaviour platform in movingPlatforms)
        {
            platform.Reset();
        }
    }

    void RetryRound()
    {
        movementCommandsList.Add(commandRecorder.movementCommands);

        foreach (var entity in entities)
        {
            Destroy(entity);
        }

        entities.Clear();

        BeginRound();
    }

    void SpawnMonster()
    {
        foreach (Transform monsterSpawn in monsterSpawns)
        {
            GameObject currentMonster = Instantiate(monsterPrefab, monsterSpawn.position, Quaternion.identity);
            entities.Add(currentMonster);
        }
    }

    void SpawnPlayer()
    {
        GameObject currentPlayer = Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity);

        currentPlayer.GetComponent<LifeManager>().OnDeath.AddListener(RetryRound);
        commandRecorder = currentPlayer.GetComponent<CommandRecorder>();
        commandRecorder.StartRecording();
        entities.Add(currentPlayer);
    }

    void SpawnPlayerClones()
    {
        foreach (var movementCommands in movementCommandsList)
        {
            GameObject currentGhostPlayer = Instantiate(ghostPlayerPrefab, playerSpawn.position, Quaternion.identity);
            currentGhostPlayer.GetComponent<PlayerGhostController>().commands = movementCommands;
            currentGhostPlayer.GetComponent<LifeManager>().OnDeath.AddListener(() => Destroy(currentGhostPlayer));
            entities.Add(currentGhostPlayer);
        }
    }

    void OnGoalReached()
    {
        SceneManager.LoadScene(nextLevelIndex);
    }
}
