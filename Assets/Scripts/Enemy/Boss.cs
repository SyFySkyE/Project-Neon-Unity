using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private int health = 100;

    public event System.Action OnSpawn;
    public event System.Action OnDestroy;

    private void OnEnable()
    {
        OnSpawn();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);        
    }

    private void OnDisable()
    {
        OnDestroy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
