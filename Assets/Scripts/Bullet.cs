﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Move Speed, usually set by Gun/Fire Controller")]
    [SerializeField] private float speed = 5f; // Usually set by GunController using Set below

    private float secBeforeDestroy = 10f;
    public float Speed
    {
        get { return this.speed; }
        set
        {
            if (value != this.speed)
            {
                this.speed = value;
            }
        }
    }

    private Rigidbody bulletRb;

    private void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, secBeforeDestroy); // Bullet destroys self during collision, this is for an extreme edge case where barrier doesn't
    }

    private void FixedUpdate()
    {
        bulletRb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        speed = -speed;
    }
}
