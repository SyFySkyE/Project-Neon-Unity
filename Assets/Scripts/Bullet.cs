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

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
