using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private static event System.Action OnMovementInput;

    public static void OnLeftMouse()
    {
        OnMovementInput?.Invoke();
    }

    private void OnEnable()
    {
        OnMovementInput += Move;
    }

    private void OnDisable()
    {
        OnMovementInput -= Move;
    }

    private void Move()
    {
        print("test");
    }
}
