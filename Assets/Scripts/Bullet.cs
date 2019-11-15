using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Move Speed")]
    [SerializeField] private float speed = 5f;
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

    private void Start()
    {
        Destroy(this.gameObject, 5f); // TODO If this becomes a rigibody, change this to destory whenever it collides (with enemy, barriers)
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
