using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float gravityMultiplier = 2f;

    private PlayerMovement player;
    private NavMeshAgent navMesh;
    
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityMultiplier;
        player = FindObjectOfType<PlayerMovement>();
        navMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player) // Null check if player died
        {
            if (navMesh) // Null check since shoot robots don't need nav mesh
            {
                navMesh.SetDestination(player.transform.position);
            }            
            transform.LookAt(player.transform); // Looks at player Y, if player Y changes this can get borked
        }        
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
