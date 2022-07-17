using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Vector2 direction = Vector2.zero;
    private LayerMask target;
    
    // Timers for removing the bullet once its out of the screen
    private float startTime = 0f;
    protected float lifeTime = 5f;

    private void Awake()
    {
        startTime = Time.time;
    }

    public void Initialize(Vector2 direction, LayerMask target)
    {
        this.direction = direction;
        this.target = target;
    }

    private void FixedUpdate()
    {
        // Moving the bullet
        transform.position += (Vector3) direction * speed;
        // Destroying the bullet once its out of life time (which is also used to remove bullets once they're probably off screen)
        if (Time.time > (startTime + lifeTime))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    virtual protected void TargetHit()
    {

    }
}
