using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Move Speed")]
    [SerializeField] private float speed = 5f;

    private float secBeforeDestroy = 5f;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        bulletRb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrier") || other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
