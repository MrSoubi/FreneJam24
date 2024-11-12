using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<List<MovementCommand>> movementCommandsList = new List<List<MovementCommand>>();

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject ghostPlayerPrefab;
    [SerializeField] GameObject monsterPrefab;
    [SerializeField] Transform playerSpawn;
    [SerializeField] Transform monsterSpawn;
    [SerializeField] GoalBehaviour goal;

    CommandRecorder commandRecorder;

    List<GameObject> entities = new List<GameObject>();


    private void Start()
    {
        goal.OnActivation.AddListener(OnGoalReached);

        BeginRound();
    }

    void BeginRound()
    {
        SpawnMonster();
        SpawnPlayerClones();
        SpawnPlayer();
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
        GameObject currentMonster = Instantiate(monsterPrefab, monsterSpawn.position, Quaternion.identity);
        entities.Add(currentMonster);
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
        Debug.Log("Win");
    }
}
