using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallSomethingEvent : MonoBehaviour
{
    public event Action<CallSomethingEvent, CallSomethingEventArgs> OnCallSomething;

    public void CallCallSomethingEvent(GameObject prefab, Transform places)
    {
        OnCallSomething?.Invoke(this, new CallSomethingEventArgs { prefab = prefab, places = places});
    }
}

public class CallSomethingEventArgs : EventArgs
{
    public GameObject prefab;
    public Transform places;
}
