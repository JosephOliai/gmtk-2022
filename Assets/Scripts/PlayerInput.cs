using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private static PlayerInput _instance;
    public static PlayerInput Instance { get { return _instance; } }

    [HideInInspector] public Controls controls;
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
        PlayerShooting.OnLeftMouse();
    }

    public static void RightMouseInput()
    {
    }
}
