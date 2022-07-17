using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    // Singleton code
    #region singleton code
    private static PlayerInput _instance;
    public static PlayerInput Instance { get { return _instance; } }

    private bool wasInstance = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    // Input handling code
    [HideInInspector] public Controls controls;

    private void Start()
    {
        controls = new Controls();
        controls.Gameplay.LMouse.performed += _ => LeftMouseInput();
        controls.Gameplay.RMouse.performed += _ => RightMouseInput();
        controls.Gameplay.Enable();
        wasInstance = true;
    }

    private void OnDestroy()
    {
        if (wasInstance)
        {
            controls.Gameplay.LMouse.performed -= _ => LeftMouseInput();
            controls.Gameplay.RMouse.performed -= _ => RightMouseInput();
            controls.Gameplay.Disable();
        }
    }

    public static void LeftMouseInput()
    {
        PlayerActionQueue.PlayerActionInput(new PlayerAction(PlayerActionType.shoot, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue())));
    }

    public static void RightMouseInput()
    {
        PlayerActionQueue.PlayerActionInput(new PlayerAction(PlayerActionType.move, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue())));
    }
}
