using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float timeToSpawn;
    private float spawnCounter;

    public Transform minSpawnPoint, maxSpawnPoint;

    public List<WaveInfo> waves;

    private int currentWave;
    private float waveCounter;

    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();
    private List<GameObject> playerAttackerEnemies = new List<GameObject>();

    private bool isBossWave = false;
    private GameObject currentBoss;

    private void Awake()
    {
        GameManager.Instance.EnemySpawner = this;
    }

    void Start()
    {
        currentWave = -1;
        GoToNextWave();
        UIController.Instance.UpdateWaveUI();
    }

    void Update()
    {
        // Check if the player is still alive
        if (PlayerHealth.instance.gameObject.activeSelf)
        {
            if (isBossWave)
            {
                CheckBossDefeated();

                spawnCounter -= Time.deltaTime;

                if (spawnCounter <= 0)
                {
                    spawnCounter = waves[currentWave].timeBetweenSpawns;

                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);

                    spawnedEnemies.Add(newEnemy);
                }
            }
            else
            {
                SpawnEnemyWave();
            }
        }
    }

    void SpawnEnemyWave()
    {
        if (currentWave < waves.Count)
        {
            waveCounter -= Time.deltaTime;
            if (waveCounter <= 0)
            {
                GoToNextWave();
            }

            spawnCounter -= Time.deltaTime;

            if (spawnCounter <= 0)
            {
                spawnCounter = waves[currentWave].timeBetweenSpawns;

                GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);

                spawnedEnemies.Add(newEnemy);
            }
        }
    }

    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;

        bool spawnVerticalEdge = Random.Range(0f, 1f) > .5f;

        if (spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minSpawnPoint.position.y, maxSpawnPoint.position.y);

            spawnPoint.x = Random.Range(0f, 1f) > .5f ? maxSpawnPoint.position.x : minSpawnPoint.position.x;
        }
        else
        {
            spawnPoint.x = Random.Range(minSpawnPoint.position.x, maxSpawnPoint.position.x);

            spawnPoint.y = Random.Range(0f, 1f) > .5f ? maxSpawnPoint.position.y : minSpawnPoint.position.y;
        }

        return spawnPoint;
    }

    public void GoToNextWave()
    {
        currentWave++;
        UIController.Instance.UpdateWaveUI();

        // Check for boss waves
        if (currentWave == 4 || currentWave == 9 || currentWave == 14 || currentWave == 19)
        {
            SpawnBoss();
            waveCounter = waves[currentWave].waveLength;
            spawnCounter = waves[currentWave].timeBetweenSpawns;
        }
        else
        {
            // If player reaches the last wave, repeat the last wave
            if (currentWave >= waves.Count)
            {
                //show victory screen here
              //  UIController.Instance.ShowVictoryScreen();
                //currentWave = waves.Count - 1;
            }

            waveCounter = waves[currentWave].waveLength;
            spawnCounter = waves[currentWave].timeBetweenSpawns;
        }
    }

    void SpawnBoss()
    {
        isBossWave = true;

        currentBoss = Instantiate(waves[currentWave].bossToSpawn, SelectSpawnPoint(), Quaternion.identity);
    }

    void CheckBossDefeated()
    {
        if (currentBoss == null) // Boss is defeated
        {
            isBossWave = false;
            GoToNextWave();
        }
    }

    public void playerDead()
    {
        foreach (var enemy in spawnedEnemies)
        {
            EnemyController _enemyController = enemy.GetComponent<EnemyController>();
            if (_enemyController.playerAttacker)
            {
                playerAttackerEnemies.Add(enemy);
                _enemyController.playerAttacker = false;
            }
        }
    }

    public void playerAlive()
    {
        foreach (var enemy in playerAttackerEnemies)
        {
            EnemyController _enemyController = enemy.GetComponent<EnemyController>();
            _enemyController.playerAttacker = true;
        }
    }

    public void ResetEnemySpawner()
    {
        foreach (var enemy in spawnedEnemies)
        {
            Destroy(enemy);
        }
        spawnedEnemies.Clear();

        currentWave = -1;
        GoToNextWave();
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }
}

[System.Serializable]
public class WaveInfo
{
    public GameObject enemyToSpawn;
    public GameObject bossToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
}
