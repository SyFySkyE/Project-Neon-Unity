using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

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
        enemyRb.AddForce(transform.forward * moveSpeed, ForceMode.Impulse);
    }

    private void OnDestroy() // TODO Should this be in enemyHealth? It's in here because this script knows Player
    { // TODO Should not use GetComponent outside of Start!!
        if (player)
        {
            player.GetComponent<PlayerComboSystem>().AddToCombo();
            player.GetComponent<PlayerPoints>().IncrementPoints();
        }        
    }
}
