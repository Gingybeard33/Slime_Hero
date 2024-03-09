
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public int waveQuota; // total number of enemies to spawn in wave
        public float spawnInterval;
        public int spawnCount; // num of enemies spawned already
    }

    [System.Serializable]
    public class EnemyGroup 
    {
        public string enemyName;
        public int enemyCount;
        public int spawnCount;
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; // list of waves in game
    public int currentWaveCount;

    [Header("Spawner Attributes")]
    float spawntimer;
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxenemiesReached = false;
    public float waveInterval;

    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints; // Stores spawn points relative to player

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuota();
    }

    // Update is called once per frame
    void Update()
    {

        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0)
        {
            StartCoroutine(BeginNextWave());
        }
            spawntimer += Time.deltaTime;
            
            if(spawntimer >= waves[currentWaveCount].spawnInterval)
            {
                spawntimer = 0f;
                SpawnEnemies();
            }
        
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);
        
        if(currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota() 
    {
        int currentWaveQuota = 0;
        foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }
        
        waves[currentWaveCount].waveQuota = currentWaveQuota;
    }

    void SpawnEnemies()
    {
        // min number enemies for current wave has spawned
        if(waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxenemiesReached)
        {
            // spawn each type of enemy
            foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                // check if type has min number spawned
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if(enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxenemiesReached = true;
                        return;
                    }

                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }

        if(enemiesAlive < maxEnemiesAllowed)
        {
            maxenemiesReached = false;
        }
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }
}
