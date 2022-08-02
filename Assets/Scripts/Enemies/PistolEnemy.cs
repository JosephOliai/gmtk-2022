using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolEnemy : MonoBehaviour
{
    private Transform player;
    private float dashTimer = 0f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootTime = 0.5f;
    [SerializeField] private float dashTime = 1f;
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private float dashDuration = 1f;
    [SerializeField] private float dashToPlayerRange = 15f;
    [SerializeField] private float spreadInDegrees = 0f;
    private float dashSpeed = 0f;
    private bool hasBullet = true;
    private bool dashing = false;
    private Vector2 dashDirection = Vector2.zero;
    private int bullets = 1;
    private LayerMask targetLayers;

    private void Awake()
    {
        player = FindObjectOfType<PlayerActionQueue>().transform;
        dashSpeed = dashDistance / dashDuration;
        targetLayers = player.gameObject.layer;
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

        Vector2 shotDirection = (player.position - transform.position).normalized;
        float halfShotSpread = spreadInDegrees / 2;

        for (int i = 0; i < bullets; i++)
        {
            GameObject bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            BaseBullet bulletScript = bulletObject.GetComponent<BaseBullet>();
            Quaternion randomSpread = Quaternion.Euler(0, 0, Random.Range(0, spreadInDegrees) - halfShotSpread);
            bulletScript.Initialize(randomSpread * shotDirection, targetLayers, Color.red);
        }
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
        hasBullet = true;
        dashing = false;
        dashTimer = 0f;
        bullets = Random.Range(1, 7);
    }
}
