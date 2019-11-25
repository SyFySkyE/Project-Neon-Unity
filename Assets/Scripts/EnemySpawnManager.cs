using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Enemy Prefabs to Spawn")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject boss;

    [Header("Places to spawn Enemies")]
    [SerializeField] private List<Transform> placesToSpawn;

    [Header("Spawning Parameters")]
    [SerializeField] private float minTimeInSecBeforeSpawn = 2f;
    [SerializeField] private float maxTimeInSecBeforeSpawn = 5f;    

    [Header("Wave Paramenters")]
    [SerializeField] private int waveNumber = 1;
    [SerializeField] private int enemiesThisWave = 10;
    [SerializeField] private int wave2EnemiesToSpawn = 10;
    [SerializeField] private int wave3EnemiesToSpawn = 15;
    [SerializeField] private int maxWaves = 3;
        
    [SerializeField] private ShopController shop;

    private int numberOfEnemiesSpawned = 0;
    private int numberOfEnemiesAlive = 0;

    static EnemySpawnManager instance;
    public static EnemySpawnManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EnemySpawnManager>();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        InstantiateRandomEnemyAndLoc();
        float nextRandomSpawnTime = Random.Range(minTimeInSecBeforeSpawn, maxTimeInSecBeforeSpawn);
        yield return new WaitForSeconds(nextRandomSpawnTime);
        if (numberOfEnemiesSpawned < enemiesThisWave)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    private void InstantiateRandomEnemyAndLoc()
    {
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
        int randomLocIndex = Random.Range(0, placesToSpawn.Count);
        GameObject nextEnemy = Instantiate(enemyPrefabs[randomEnemyIndex], placesToSpawn[randomLocIndex].position, Quaternion.identity) as GameObject;
        numberOfEnemiesAlive++;
        numberOfEnemiesSpawned++;
    }    

    public void OnEnemyDeath()
    {
        numberOfEnemiesAlive--;
        if (numberOfEnemiesAlive == 0 && numberOfEnemiesSpawned >= enemiesThisWave)
        {
            shop.BroadcastMessage("WaveComplete");
        }
    }

    public void RestartWave()
    {
        numberOfEnemiesSpawned = 0;
        StartCoroutine(SpawnEnemy());
    }

    public void NextWave()
    {
        if (waveNumber == 1)
        {
            waveNumber++;
            numberOfEnemiesSpawned = 0;
            enemiesThisWave = wave2EnemiesToSpawn;
            StartCoroutine(SpawnEnemy());
        }
        else if (waveNumber == 2)
        {
            waveNumber++;
            numberOfEnemiesSpawned = 0;
            enemiesThisWave = wave3EnemiesToSpawn;
            StartCoroutine(SpawnEnemy());
        }
        else if (waveNumber >= maxWaves)
        {
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        boss.SetActive(true);
    }
}
