using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Points")]
    [SerializeField] private int healthPoints = 3;

    private Animator enemyAnim;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            HurtEnemy();
            enemyAnim.SetTrigger("HurtTrigger");
            Destroy(other.gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        HurtEnemy();
        enemyAnim.SetTrigger("HurtTrigger");
    }

    private void HurtEnemy()
    {
        healthPoints--;
        if (healthPoints <= 0)
        {
            EnemySpawnManager.Instance.OnEnemyDeath();
            Destroy(gameObject);
        }
    }
}
