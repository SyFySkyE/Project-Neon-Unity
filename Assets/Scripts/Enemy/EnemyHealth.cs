using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Points")]
    [SerializeField] private int healthPoints = 3;

    private bool isLastEnemy = false;

    private Animator enemyAnim;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoints <= 0)
        {
            if (this.isLastEnemy)
            {
                BroadcastMessage("WaveComplete");
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            healthPoints--;
            enemyAnim.SetTrigger("HurtTrigger");
            Destroy(other.gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        healthPoints--;
        enemyAnim.SetTrigger("HurtTrigger");
    }

    public void EnableLastEnemyTag()
    {
        this.isLastEnemy = true;
    }
}
