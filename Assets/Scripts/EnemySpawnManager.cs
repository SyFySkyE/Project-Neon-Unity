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
    [SerializeField] private int maxNumberOfEnemies = 3;

    [Header("Wave Paramenters")]
    [SerializeField] private int waveNumber = 1;
    [SerializeField] private int enemiesThisWave = 10;
    [SerializeField] private int wave1EnemiesToSpawn = 5;
    [SerializeField] private int wave2EnemiesToSpawn = 10;
    [SerializeField] private int wave3EnemiesToSpawn = 15;
        
    [SerializeField] private GameSceneManager sceneManagement;

    private int numberOfEnemiesSpawned = 0;
    private int numberOfEnemiesAlive = 0;

    public event System.Action OnWaveComplete;

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

    private void OnEnable()
    {
        if (boss)
        {
            boss.GetComponent<Boss>().OnDestroy += EnemySpawnManager_OnDestroy;
        }        
    }

    private void EnemySpawnManager_OnDestroy()
    {
        sceneManagement.LoadNextScene();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        if (numberOfEnemiesSpawned < enemiesThisWave)
        {
            InstantiateRandomEnemyAndLoc();
            float nextRandomSpawnTime = Random.Range(minTimeInSecBeforeSpawn, maxTimeInSecBeforeSpawn);
            yield return new WaitForSeconds(nextRandomSpawnTime);
            if (numberOfEnemiesSpawned < enemiesThisWave)
            {
                StartCoroutine(SpawnEnemy());
            }
        }       
    }

    private void InstantiateRandomEnemyAndLoc()
    {
        if (numberOfEnemiesAlive < maxNumberOfEnemies)
        {
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            int randomLocIndex = Random.Range(0, placesToSpawn.Count);
            GameObject nextEnemy = Instantiate(enemyPrefabs[randomEnemyIndex], placesToSpawn[randomLocIndex].position, Quaternion.identity) as GameObject;
            numberOfEnemiesAlive++;
            numberOfEnemiesSpawned++;
        }        
    }    

    public void OnEnemyDeath()
    {
        if (this.waveNumber != 0) // If this is NOT the tutorial level
        {
            numberOfEnemiesAlive--;
        }
        
        if (numberOfEnemiesAlive <= 0 && numberOfEnemiesSpawned >= enemiesThisWave)
        {
            OnWaveComplete();
        }
    }

    public void RestartWave()
    {
        numberOfEnemiesSpawned = 0;
        StartCoroutine(SpawnEnemy());
    }

    public void NextWave() 
    {
        switch (waveNumber)
        {
            case 0:
                waveNumber++;
                numberOfEnemiesSpawned = 0;
                enemiesThisWave = wave1EnemiesToSpawn;
                StartCoroutine(SpawnEnemy());
                break;
            case 1:
                waveNumber++;
                numberOfEnemiesSpawned = 0;
                enemiesThisWave = wave2EnemiesToSpawn;
                StartCoroutine(SpawnEnemy());
                break;
            case 2:
                waveNumber++;
                numberOfEnemiesSpawned = 0;
                enemiesThisWave = wave3EnemiesToSpawn;
                StartCoroutine(SpawnEnemy());
                break;
            case 3:
                SpawnBoss();
                break;
        }
    }

    private void SpawnBoss()
    {
        if (boss)
        {
            boss.SetActive(true);
        }
        else
        {
            sceneManagement.LoadNextScene();
        }
    }
}
