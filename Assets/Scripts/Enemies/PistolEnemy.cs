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
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private int direction = 1;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerActionQueue>().transform;
        dashSpeed = dashDistance / dashDuration;
        targetLayers = player.gameObject.layer;

        setDirection(player.transform.position);
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
        animator.SetBool("isShooting", true);

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

    public void SetIdle(int direction)
    {
        animator.SetInteger("Direction", direction);
        animator.SetBool("isShooting", false);
    }

    private void StartDash()
    {
        dashing = true;
        animator.SetBool("isRoling", dashing);
        if (Vector2.Distance(player.position, transform.position) > dashToPlayerRange)
        {
            dashDirection = (player.position - transform.position).normalized;
            setDirection(dashDirection);
        }
        else
        {
            dashDirection = (Quaternion.Euler(0, 0, 65) * (player.position - transform.position).normalized);
            setDirection(dashDirection);
        }
    }

    private void setDirection(Vector2 direction)
    {
        float angle = Vector2.SignedAngle(direction, transform.up);

        if (angle < 0) angle = 360 - angle * -1;

        //print(angle);
        if (angle >= 0 && angle < 90)
        {
            // right up
            this.direction = 1;
            spriteRenderer.flipX = false;
        }
        if (angle >= 90 && angle < 180)
        {
            // right down
            this.direction = 0;
            spriteRenderer.flipX = false;
        }
        if (angle >= 180 && angle < 270)
        {
            // left down
            this.direction = 0;
            spriteRenderer.flipX = true;
        }
        if (angle >= 270 && angle < 360)
        {
            // left up
            this.direction = 1;
            spriteRenderer.flipX = true;
        }
        //print(this.direction);
        animator.SetInteger("Direction", this.direction);
    }

    private void EndDash()
    {
        Vector2 tempDirection = (player.position - transform.position).normalized;
        setDirection(tempDirection);

        hasBullet = true;
        dashing = false;
        animator.SetBool("isRoling", dashing);
        dashTimer = 0f;
        bullets = Random.Range(1, 7);
    }
}
