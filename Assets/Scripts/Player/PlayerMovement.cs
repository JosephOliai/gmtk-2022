using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private PlayerActionQueue playerActionQueue;
    private PlayerShooting playerShooting;

    private Vector2 targetPosition;
    private Vector2 previousPosition;
    private bool rolling = false;
    private int direction = 1;

    [SerializeField] private float dashDuration = 0.33f;
    [SerializeField] private float dashLeeway = 0.2f;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        playerActionQueue = GetComponent<PlayerActionQueue>();
        playerShooting = GetComponent<PlayerShooting>();

        dashDuration = dashDuration / Time.deltaTime;
        dashLeeway *= 100;
    }

    private void Update()
    {
        if (rolling)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, Mathf.Abs(Mathf.Sqrt(Mathf.Pow(targetPosition.y - previousPosition.y, 2) + Mathf.Pow(targetPosition.x - previousPosition.x, 2)) / dashDuration));
        }
        if (rolling && Vector2.Distance(transform.position, targetPosition) < dashLeeway)
        {
            rolling = false;
            playerActionQueue.ActionFinished();
            playerShooting.RandomizeAndReloadBullet();
            animator.SetBool("rolling", false);
        }
    }

    public void Move(Vector2 targetPosition)
    {
        previousPosition = transform.position;
        rolling = true;
        animator.SetBool("rolling", true);
        this.targetPosition = targetPosition;

        // animation
        float angle = (Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * 180) / Mathf.PI;
        if (angle < 0) angle += 360f;

        if (angle >= 0 && angle < 90)
        {
            print("right up");
            direction = 4;
        }
        if (angle >= 90 && angle < 180)
        {
            print("left up");
            direction = 3;
        }
        if (angle >= 180 && angle < 270)
        {
            print("left down");
            direction = 2;
        }
        if (angle >= 270 && angle < 360)
        {
            print("right down");
            direction = 1;
        }

        animator.SetInteger("direction", direction);
    }
}
