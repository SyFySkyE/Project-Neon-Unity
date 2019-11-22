using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement stats and dependencies")]
    [SerializeField] private float moveSpeed = 5f;

    private PlayerMovement player;
    private Rigidbody enemyRb;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player) // Null check if player died
        {
            transform.LookAt(player.transform); // Looks at player Y, if player Y changes this can get borked
        }        
    }

    private void FixedUpdate()
    {
        enemyRb.velocity = transform.forward * moveSpeed;
    }

    private void OnDestroy() // TODO Should this be in enemyHealth? It's in here because this script knows Player
    {
        if (player)
        {
            player.GetComponent<PlayerComboSystem>().AddToCombo();
            player.GetComponent<PlayerPoints>().IncrementPoints();
        }        
    }
}
