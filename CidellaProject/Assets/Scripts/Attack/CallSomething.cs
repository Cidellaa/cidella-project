using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CallSomethingEvent))]
[DisallowMultipleComponent]
public class CallSomething : MonoBehaviour
{
    private CallSomethingEvent callSomethingEvent;

    private void Awake()
    {
        callSomethingEvent = GetComponent<CallSomethingEvent>();
    }

    private void OnEnable()
    {
        callSomethingEvent.OnCallSomething += CallSomethingEvent_OnCallSomething;
    }

    private void OnDisable()
    {
        callSomethingEvent.OnCallSomething -= CallSomethingEvent_OnCallSomething;
    }

    private void CallSomethingEvent_OnCallSomething(CallSomethingEvent callSomethingEvent, CallSomethingEventArgs callSomethingEventArgs)
    {
        CreateObjects(callSomethingEventArgs.prefab, callSomethingEventArgs.places);
    }

    private void CreateObjects(GameObject prefab, Transform places)
    {
        for (int i = 0; i < places.childCount; i++)
        {
            Instantiate(prefab, places.GetChild(i).position, Quaternion.identity);
        }
    }
}
