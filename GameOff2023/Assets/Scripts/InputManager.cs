using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PinballActions _PA;

    public static Action<bool> LeftFlipperTriggered;
    public static Action<bool> RightFlipperTriggered;
    public static Action LaunchTriggered;
    public static Action PauseMenuTriggered;

    private void Awake()
    {
        _PA = new PinballActions();
    }

    private void OnEnable()
    {
        _PA.Player.Enable();
        _PA.Player.LeftFlipper.performed += LeftFlipperOn;
        _PA.Player.RightFlipper.performed += RightFlipperOn;
        _PA.Player.LeftFlipper.canceled += LeftFlipperOff;
        _PA.Player.RightFlipper.canceled += RightFlipperOff;
        _PA.Player.Pause.performed += PauseMenuOpen;
        _PA.Player.Launch.performed += LaunchBall;
    }
    private void OnDisable()
    {
        _PA.Player.LeftFlipper.performed -= LeftFlipperOn;
        _PA.Player.RightFlipper.performed -= RightFlipperOn;
        _PA.Player.LeftFlipper.canceled -= LeftFlipperOff;
        _PA.Player.RightFlipper.canceled -= RightFlipperOff;
        _PA.Player.Pause.performed -= PauseMenuOpen;
        _PA.Player.Disable();
    }

    private void LeftFlipperOn(InputAction.CallbackContext context)
    {
        LeftFlipperTriggered.Invoke(true);
    }

    private void RightFlipperOn(InputAction.CallbackContext context)
    {
        RightFlipperTriggered.Invoke(true);
    }

    private void LeftFlipperOff(InputAction.CallbackContext context)
    {
        LeftFlipperTriggered.Invoke(false);
    }

    private void RightFlipperOff(InputAction.CallbackContext context)
    {
        RightFlipperTriggered.Invoke(false);
    }

    public void PauseMenuOpen(InputAction.CallbackContext context)
    {
        PauseMenuTriggered.Invoke();
    }

    public void LaunchBall(InputAction.CallbackContext context)
    {
        LaunchTriggered?.Invoke();
    }
}
