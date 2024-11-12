using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyToSpawn;
    public float timeToSpawn;
    private float spawnCounter;

    public Transform minSpawnPoint, maxSpawnPoint;

    public List<WaveInfo> waves;

    private int currentWave;
    private float waveCounter;
    private List<GameObject> spawnedEnemies = new List<GameObject>();   

    private List<GameObject> playerAttackerEnemies = new List<GameObject>();

    void Start()
    {
        //spawnCounter = timeToSpawn;

        currentWave = -1;
        GoToNextWave();
    }

    void Update()
    {
        // check if player still alive
        if (PlayerHealth.instance.gameObject.activeSelf)
        {
            SpawnEnemyWave();
        }

       //SpawnEnemy();
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

    void SpawnEnemy()
    {
        //If we do wave based spawning then we need to set the amount of enemies per wave and what enemies to spawn

        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeToSpawn;

            Instantiate(enemyToSpawn, SelectSpawnPoint(), transform.rotation);
        }
    }

    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;

        bool spawnVerticalEdge = Random.Range(0f, 1f) > .5f;

        if (spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minSpawnPoint.position.y, maxSpawnPoint.position.y);

            if(Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.x = maxSpawnPoint.position.x;
            }
            else
            {
                spawnPoint.x = minSpawnPoint.position.x;

            }
        }
        else
        {
            spawnPoint.x = Random.Range(minSpawnPoint.position.x, maxSpawnPoint.position.x);

            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.y = maxSpawnPoint.position.y;
            }
            else
            {
                spawnPoint.y = minSpawnPoint.position.y;

            }
        }

        return spawnPoint;
    }

    public void GoToNextWave()
    {
        currentWave++;

        //if player reaches last wave, repeat last wave
        if(currentWave >= waves.Count)
        {
            currentWave = waves.Count - 1;
        }

        waveCounter = waves[currentWave].waveLength;
        spawnCounter = waves[currentWave].timeBetweenSpawns;
    }

    public void playerDead()
    {
        for (int i = 0; i < spawnedEnemies.Count; ++i)
        {
           EnemyController _enemyController = spawnedEnemies[i].GetComponent<EnemyController>();
           if (_enemyController.playerAttacker == true)
           {
            playerAttackerEnemies.Add(spawnedEnemies[i]);
            _enemyController.playerAttacker = false;
           }
        }
    }

    public void playerAlive()
    {
        for (int i = 0; i < playerAttackerEnemies.Count; ++i)
        {
            EnemyController _enemyController = playerAttackerEnemies[i].GetComponent<EnemyController>();
            _enemyController.playerAttacker = true;
        }
    }
}



[System.Serializable]
public class WaveInfo
{
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
}
