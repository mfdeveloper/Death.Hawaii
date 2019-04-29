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

    public static void Down(string buttonName, Action<Vector3> callback, GameObject objReference = null)
    {
        Vector3 position = GetTouchOrClick(buttonName, objReference);

        if (position != Vector3.zero)
        {
            callback(position);
        } else if (Input.GetButtonDown(buttonName))
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

    public static Vector3 GetTouchOrClick(string buttonName = "Horizontal", GameObject objReference = null) {

        Vector3 position = Vector3.zero;

        if (Input.touches.Length > 0) 
        {
            
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                position = touch.position;
            }

            //TODO: Refactor this to allow GetMouseButton too (For Horizontal movement)
        } else if (Input.GetMouseButtonDown(0)) {
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
        } else if (buttonName.Equals("Vertical"))
        {
            if (position != Vector3.zero)
            {
                if (objReference != null)
                {
                    var worldPosition = Camera.main.ScreenToWorldPoint(position);
                    if (worldPosition.y < objReference.transform.position.y)
                    {
                        Debug.Log("DOWN");
                        position = new Vector3(position.x, -1);
                    } else if (worldPosition.y > objReference.transform.position.y)
                    {
                        Debug.Log("UP");
                        position = new Vector3(position.x, 1);
                    }
                }
            }
        }

        return position;
    }
}
