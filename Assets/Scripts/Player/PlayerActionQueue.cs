using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerActionType
{
    move,
    shoot,
}

public struct PlayerAction
{
    public PlayerAction(PlayerActionType playerActionType, Vector2 target)
    {
        this.playerActionType = playerActionType;
        this.target = target;
    }

    public PlayerActionType playerActionType;
    public Vector2 target;
}

public class PlayerActionQueue : MonoBehaviour
{

    // Input receiving code
    private static event System.Action<PlayerAction> OnPlayerInput;
    public static void PlayerActionInput(PlayerAction playerAction)
    {
        OnPlayerInput?.Invoke(playerAction);
    }
    private void OnEnable()
    {
        OnPlayerInput += HandlePlayerInput;
    }
    private void OnDisable()
    {
        OnPlayerInput -= HandlePlayerInput;
    }

    // Queue management code
    private Queue<PlayerAction> actionQueue;
    private PlayerMovement playerMovement;
    private PlayerShooting playerShooting;
    private bool actionActive = false;

    private void Awake()
    {
        actionQueue = new Queue<PlayerAction>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponent<PlayerShooting>();
    }

    // Gets called any time the player presses the left or right mouse button
    private void HandlePlayerInput(PlayerAction playerAction)
    {
        actionQueue.Enqueue(playerAction);
    }

    private void FixedUpdate()
    {
        // Only execute an action from the queue if there is an action and no action is being executed
        if (actionQueue.Count != 0 && !actionActive)
        {
            PlayerAction currentAction = actionQueue.Dequeue();
            if (currentAction.playerActionType == PlayerActionType.move)
            {
                playerMovement.Move(currentAction.target);
                actionActive = true;
            } else // if playeractiontype = shoot
            {
                playerShooting.Shoot(currentAction.target);
            }
        }
    }

    public void ClearActionQueue()
    {
        actionQueue.Clear();
    }

    public void ActionFinished()
    {
        actionActive = false;
    }
}
