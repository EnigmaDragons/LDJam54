﻿using System;
using System.Linq;
using UnityEngine;

public class ControllerWatcher : MonoBehaviour
{
    [SerializeField] private ControlType overridenControlType;
    [SerializeField] private bool shouldOverride;
    [SerializeField] private FloatReference minimumMovementBeforeDirectionCounted;
    private static KeyCode[] _keyboardButtons = {KeyCode.KeypadEnter, KeyCode.Return, KeyCode.Escape, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Space, KeyCode.Tab, KeyCode.A, KeyCode.W, KeyCode.S, KeyCode.D, KeyCode.Keypad2, KeyCode.Keypad4, KeyCode.Keypad6, KeyCode.Keypad8};
    private static KeyCode[] _verticalKeyboard = { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.W, KeyCode.S };
    private static KeyCode[] _horizontalKeyboard = { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.A, KeyCode.D };
    private static string[] _joystickButtons = {"joystick button 0", "joystick button 1", "joystick button 2", "joystick button 3", "joystick button 4", "joystick button 5"};
    private static string[] _joystickAxis = { "Axis10", "Axis6", "Axis9", "Axis3" };

    private float _verticalAxis;
    private float _horizontalAxis;

    private void Start()
    {
        if (PlayerPrefs.HasKey("InputControlType"))
        {
            InputControl.Type = (ControlType)PlayerPrefs.GetInt("InputControlType");
            Message.Publish(new InputControlChanged());
        }
    }
    
    private void Update()
    {
        var vertical = Math.Max(Math.Abs(Input.GetAxisRaw("Vertical")), Math.Abs(Input.GetAxisRaw("Axis7")));
        var horizontal = Math.Max(Math.Abs(Input.GetAxisRaw("Horizontal")), Math.Abs(Input.GetAxisRaw("Axis6")));
        if (shouldOverride && InputControl.Type != overridenControlType)
        {
            InputControl.Type = overridenControlType;
            Message.Publish(new InputControlChanged());
        }
        else if (shouldOverride)
            return;
        else if (InputControl.Type != ControlType.Mouse && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
        {
            InputControl.Type = ControlType.Mouse;
            SaveInput();
            Message.Publish(new InputControlChanged());
        }
        else if (InputControl.Type != ControlType.Keyboard && _keyboardButtons.Any(Input.GetKeyDown))
        {
            InputControl.Type = ControlType.Keyboard;
            SaveInput();
            Message.Publish(new InputControlChanged());
        }
        else if (InputControl.Type != ControlType.Xbox && InputControl.Type != ControlType.Playstation && InputControl.Type != ControlType.Switch && (
               _joystickButtons.Any(Input.GetKeyDown) 
            || _joystickAxis.Any(x => Input.GetAxis(x) >= 1 
            || (_verticalAxis < minimumMovementBeforeDirectionCounted.Value && vertical >= minimumMovementBeforeDirectionCounted.Value && _verticalKeyboard.All(key => !Input.GetKey(key)))
            || (_horizontalAxis < minimumMovementBeforeDirectionCounted.Value && horizontal >= minimumMovementBeforeDirectionCounted.Value && _horizontalKeyboard.All(key => !Input.GetKey(key))))))
        {
            var joysticks = Input.GetJoystickNames();
            if (joysticks[0].ToLower().Contains("playstation"))
                InputControl.Type = ControlType.Playstation;
            else if (joysticks[0].ToLower().Contains("xbox"))
                InputControl.Type = ControlType.Xbox;
            else
                InputControl.Type = ControlType.Gamepad;
            SaveInput();
            Message.Publish(new InputControlChanged());
        }
        _verticalAxis = vertical;
        _horizontalAxis = horizontal;
    }
    
    private void SaveInput() => PlayerPrefs.SetInt("InputControlType", (int)InputControl.Type);
}