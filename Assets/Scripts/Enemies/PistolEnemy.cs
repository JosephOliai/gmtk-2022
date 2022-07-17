using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolEnemy : MonoBehaviour
{
    private Transform player;
    private float dashTimer = 0f;
    [SerializeField] private float shootTime = 0.5f;
    [SerializeField] private float dashTime = 1f;
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private float dashDuration = 1f;
    [SerializeField] private float dashToPlayerRange = 15f;
    private float dashSpeed = 0f;
    private bool hasBullet = true;
    private bool dashing = false;
    private Vector2 dashDirection = Vector2.zero;

    private void Awake()
    {
        player = FindObjectOfType<PlayerActionQueue>().transform;
        dashSpeed = dashDistance / dashDuration;
    }

    private void FixedUpdate()
    {
        dashTimer += Time.deltaTime;
        MoveIfDashing();

        if (dashTimer > shootTime && hasBullet) // SHOOTING CODE
        {
            Shoot();
        } else if (dashTimer > dashTime && !dashing) // DASH START CODE
        {
            StartDash();
        } else if (dashTimer > dashTime + dashDuration) // DASH END CODE
        {
            EndDash();
        }
    }

    private void MoveIfDashing()
    {
        if (dashing)
        {
            transform.position += (Vector3)(dashDirection * dashSpeed * Time.fixedDeltaTime);
        }
    }

    private void Shoot()
    {
        hasBullet = false;
    }

    private void StartDash()
    {
        dashing = true;
        if (Vector2.Distance(player.position, transform.position) > dashToPlayerRange)
        {
            dashDirection = (player.position - transform.position).normalized;
        }
        else
        {
            dashDirection = (Quaternion.Euler(0, 0, 65) * (player.position - transform.position).normalized);
        }
    }

    private void EndDash()
    {
        //TODO: end dash reset timer and booleans
        hasBullet = true;
        dashing = false;
        dashTimer = 0f;
    }
}
