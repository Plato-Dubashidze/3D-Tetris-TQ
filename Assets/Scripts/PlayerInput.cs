using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private bool isMobile;


    //Swipe
    private Vector2 tapPosition;
    private Vector2 swipeDelta;

    private float deadZone = 80;

    private bool isSwiping;

    //Double Click
    private const float doubleClickTime = .2f;
    private float lastClickTime;

    private void Start()
    {
        isMobile = Application.isMobilePlatform;
    }

    private void Update()
    {
        //Swipe
        if (!isMobile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSwiping = true;
                tapPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
                ResetSwipe();

            CheckKeyInput();
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    isSwiping = true;
                    tapPosition = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    ResetSwipe();
                }
            }
        }
        CheckSwipe();

        //Double Click
        if (!isMobile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                float timeSinceLastClick = Time.time - lastClickTime;
                if (timeSinceLastClick <= doubleClickTime)
                {
                    GlobalEventManager.DoubleTap();
                }

                lastClickTime = Time.time;
            }
        }

        if (isMobile)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase.Equals(TouchPhase.Ended))
                {
                    float timeSinceLastClick = Time.time - lastClickTime;
                    if (timeSinceLastClick <= doubleClickTime)
                    {
                        GlobalEventManager.DoubleTap();
                    }

                    lastClickTime = Time.time;
                }
            }
        }
    }
    private void CheckSwipe()
    {
        swipeDelta = Vector2.zero;

        if (isSwiping)
        {
            if (!isMobile && Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - tapPosition;
            else if (Input.touchCount > 0)
                swipeDelta = Input.GetTouch(0).position - tapPosition;
        }

        if (swipeDelta.magnitude > deadZone)
        {
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                GlobalEventManager.Swipe(swipeDelta.x > 0 ? Vector2.up : Vector2.down);

            ResetSwipe();
        }
    }

    private void ResetSwipe()
    {
        isSwiping = false;
        tapPosition = Vector2.zero;
        swipeDelta = Vector2.zero;
    }

    private void CheckKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            GlobalEventManager.Swipe(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            GlobalEventManager.Swipe(Vector2.down);
        }
    }
}
