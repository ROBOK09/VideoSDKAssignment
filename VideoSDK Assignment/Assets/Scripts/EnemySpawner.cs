using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Basic Enemy prefab

    [SerializeField]
    private GameObject enemyPrefab;

    private EnemyFactory enemyFactory;

    private GameObject enemyGameObject;

    private void Start()
    {
        // Subscribing to various events fired on Game State Changes and events to spawn new Enemy (Pub - Sub Pattern)

        Enemy.hitTarget += SpawnNewRandomEnemy;
        Enemy.missTarget += SpawnNewRandomEnemy;
        UIManager.Instance.GameStarted += InitialEnemySpawnStart;
        UIManager.Instance.GameRestarted += ResetEnemySpawner;
        UIManager.Instance.GameOver += DestroyEnemySpawner;
    }

    private void InitialEnemySpawnStart()
    {
        // Initializing a new Enemy Factory on Game Start

        enemyFactory = new EnemyFactory();
        SpawnNewRandomEnemy();
    }

    private Vector3 NewEnemySpawnLocation()
    {
        return new Vector3(UnityEngine.Random.Range(-20, 20), 0.5f, UnityEngine.Random.Range(-20, 20));
    }

    private void SpawnNewRandomEnemy()
    {
        Vector3 pos = NewEnemySpawnLocation();
        enemyGameObject = Instantiate(enemyPrefab, pos, Quaternion.identity);
        enemyFactory.SpawnNewEnemy(enemyGameObject, (EnemyType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(EnemyType)).Length));
    }

    private void OnDisable()
    {
        // Desubscribing to various events fired on Game State Changes and events to spawn new Enemy to prevent memory leaks (Pub - Sub Pattern)

        Enemy.hitTarget -= SpawnNewRandomEnemy;
        Enemy.missTarget -= SpawnNewRandomEnemy;
        UIManager.Instance.GameStarted -= InitialEnemySpawnStart;
        UIManager.Instance.GameRestarted -= ResetEnemySpawner;
        UIManager.Instance.GameOver -= DestroyEnemySpawner;
    }

    private void ResetEnemySpawner()
    {
        DestroyEnemySpawner();
        SpawnNewRandomEnemy();
    }

    private void DestroyEnemySpawner()
    {
        if (enemyGameObject != null)
        {
            Destroy(enemyGameObject);
        }
    }
}
