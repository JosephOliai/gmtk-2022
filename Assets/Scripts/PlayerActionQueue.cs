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
    private bool actionActive = false;

    private void Awake()
    {
        actionQueue = new Queue<PlayerAction>();
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
                //TODO move
            } else
            {
                //TODO shoot
            }
        }
    }

    public void ClearActionQueue()
    {
        actionQueue.Clear();
    }
}
