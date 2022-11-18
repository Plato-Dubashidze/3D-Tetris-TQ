using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent<Vector2> swipe = new UnityEvent<Vector2>();
    public static UnityEvent doubleTap = new UnityEvent();

    public static void Swipe(Vector2 direction)
    {
        swipe.Invoke(direction);
    }

    public static void DoubleTap()
    {
        doubleTap.Invoke();
    }
}
