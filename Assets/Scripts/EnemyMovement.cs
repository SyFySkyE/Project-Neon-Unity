using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement stats and dependencies")]
    [SerializeField] private float moveSpeed = 5f;
    [Tooltip("Enemies depend on player")]
    [SerializeField] private PlayerMovement player;

    private Rigidbody enemyRb;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform); // Looks at player Y, if player Y changes this can get borked
    }

    private void FixedUpdate()
    {
        enemyRb.velocity = transform.forward * moveSpeed;
    }
}
