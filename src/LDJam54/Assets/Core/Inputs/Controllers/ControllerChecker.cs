using UnityEngine;

public static class ControllerChecker
{
    public static bool IsA() => Input.GetKeyDown("joystick button 0");
    public static bool IsB() => Input.GetKeyDown("joystick button 1");
    public static bool IsBHeld() => Input.GetKey("joystick button 1");
    public static bool IsX() => Input.GetKeyDown("joystick button 2");
    public static bool IsY() => Input.GetKeyDown("joystick button 3");
    public static bool IsMenu() => Input.GetKeyDown("joystick button 7");
    public static bool IsRightBumper() => Input.GetKeyDown("joystick button 5");
    public static bool IsPrevious() => Input.GetKeyDown("joystick button 4");

    private static bool _rightTriggerHeld;
    public static bool IsRightTrigger()
    {
        var rightTrigger = Input.GetAxis("Axis10") >= 1 || (Input.GetAxis("Axis6") >= 1 && InputControl.Type != ControlType.Xbox);
        if (rightTrigger && !_rightTriggerHeld)
        {
            _rightTriggerHeld = true;
            return true;
        }
        else if (!rightTrigger && _rightTriggerHeld)
        {
            _rightTriggerHeld = false;
        }
        return false;
    }

    private static bool _leftTriggerHeld;
    public static bool IsLeftTrigger()
    {
        var leftTrigger = Input.GetAxis("Axis9") >= 1 || Input.GetAxis("Axis3") >= 1;
        if (leftTrigger && !_leftTriggerHeld)
        {
            _leftTriggerHeld = true;
            return true;
        }
        else if (!leftTrigger && _leftTriggerHeld)
        {
            _leftTriggerHeld = false;
        }
        return false;
    }
    
    public static bool IsRightStickDown() => Input.GetKeyDown("joystick button 9");
    public static bool IsRightStickUp() => Input.GetKeyUp("joystick button 9");
    public static bool IsLeftStickDown() => Input.GetKeyDown("joystick button 8");
    public static bool IsLeftStickUp() => Input.GetKeyUp("joystick button 8");
}