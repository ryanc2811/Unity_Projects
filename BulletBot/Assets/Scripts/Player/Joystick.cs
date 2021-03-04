using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick
{
    private Vector2 startingPoint;
    private int touch = 99;

    Camera cam;

    public Joystick()
    {
        FunctionUpdater.Create(() => Update());
        cam = Camera.main;
    }
    public delegate void OnJoystickMove(Vector2 direction);
    public OnJoystickMove OnJoystickMoveCallback;
    private void MobileInput()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            Vector2 touchPos = GetTouchPosition(t.position); // * -1 for perspective cameras
            if (t.phase == TouchPhase.Began)
            {
                touch = t.fingerId;
                startingPoint = touchPos;
            }
            else if (t.phase == TouchPhase.Moved && touch == t.fingerId)
            {
                Vector2 offset = touchPos - startingPoint;
                Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
                if (OnJoystickMoveCallback != null)
                    OnJoystickMoveCallback(direction);
                //circle.transform.position = new Vector2(outerCircle.transform.position.x + direction.x, outerCircle.transform.position.y + direction.y);

            }
            else if (t.phase == TouchPhase.Ended && touch == t.fingerId)
            {
                touch = 99;
                //circle.transform.position = new Vector2(outerCircle.transform.position.x, outerCircle.transform.position.y);
            }
            ++i;
        }
    }
    private void MouseInput()
    {
        Vector2 mousePos = GetTouchPosition(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                startingPoint = mousePos;

            }
            if (Input.GetMouseButton(0))
            {
                Vector2 offset = mousePos - startingPoint;
                Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
                if(OnJoystickMoveCallback!=null)
                    OnJoystickMoveCallback(direction);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            MobileInput();
        else if (Application.platform == RuntimePlatform.WindowsEditor)
            MouseInput();
    }
    Vector2 GetTouchPosition(Vector2 touchPosition)
    {
        return cam.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, cam.transform.position.z));
    }
}
