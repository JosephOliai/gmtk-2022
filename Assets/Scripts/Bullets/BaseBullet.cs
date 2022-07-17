using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Vector2 direction = Vector2.zero;
    private LayerMask target;

    public void Initialize(Vector2 direction, LayerMask target)
    {
        this.direction = direction;
        this.target = target;
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3) direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    virtual protected void TargetHit()
    {

    }
}
