using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{

    static InputManager(){}
    
    /// <summary>
    /// Alternative way to check if is mobile: SystemInfo.deviceType == DeviceType.Handheld
    /// </summary>
    /// <param name="buttonName"></param>
    /// <param name="callback"></param>
    public static void DownHeld(string buttonName, Action<Vector3> callback ) 
    {
        
        Vector3 position = GetTouchOrClick();

        if (position != Vector3.zero)
        {
            callback(position);
        } else if(Input.GetButton(buttonName)) 
        {
            Vector3 axis = Vector3.zero;
            if (buttonName.Equals("Horizontal"))
            {
                axis = new Vector3(Input.GetAxis(buttonName), 0.0f);
            } else if (buttonName.Equals("Vertical"))
            {
                axis = new Vector3(0.0f, Input.GetAxis(buttonName));
            }

            callback(axis);
        } else {
            callback(Vector3.zero);
        }
    }

    public static void Down(string buttonName, Action<Vector3> callback)
    {
        if (Input.GetButtonUp(buttonName))
        {
            Vector3 axis = Vector3.zero;
            if (buttonName.Equals("Horizontal"))
            {
                axis = new Vector3(Input.GetAxis(buttonName), 0.0f);
            } else if (buttonName.Equals("Vertical"))
            {
                axis = new Vector3(0.0f, Input.GetAxis(buttonName));
            }

            callback(axis);
        } else {
            callback(Vector3.zero);
        }
    }

    public static Vector3 GetTouchOrClick(string buttonName = "Horizontal") {

        Vector3 position = Vector3.zero;

        if (Input.touches.Length > 0) 
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                position = touch.position;
            }
        } else if (Input.GetMouseButton(0)) {
            position = Input.mousePosition;
        }

        if (buttonName.Equals("Horizontal"))
        {    
            if (position != Vector3.zero)
            {
                if (position.x < Screen.width / 2)
                {
                    Debug.Log("LEFT");
                    position = new Vector3(-1, position.y);
                } else if (position.x > Screen.width / 2)
                {
                    Debug.Log("RIGHT");
                    position = new Vector3(1, position.y);
                }
            }
        }

        return position;
    }
}
