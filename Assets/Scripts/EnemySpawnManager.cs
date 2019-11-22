using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Enemy Prefabs to Spawn")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Places to spawn Enemies")]
    [SerializeField] private List<Transform> placesToSpawn;

    [Header("Spawning Parameters")]
    [SerializeField] private float minTimeInSecBeforeSpawn = 2f;
    [SerializeField] private float maxTimeInSecBeforeSpawn = 5f;
    [SerializeField] private int enemiesThisWave = 10;

    private int enemiesSpawned = 0;

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
        if (enemiesSpawned <= enemiesThisWave)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    private void InstantiateRandomEnemyAndLoc()
    {
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
        int randomLocIndex = Random.Range(0, placesToSpawn.Count);
        GameObject nextEnemy = Instantiate(enemyPrefabs[randomEnemyIndex], placesToSpawn[randomLocIndex].position, Quaternion.identity) as GameObject;
        enemiesSpawned++;
        if (enemiesSpawned < enemiesThisWave)
        {
            nextEnemy.GetComponent<EnemyHealth>().EnableLastEnemyTag();
        }
    }
}
