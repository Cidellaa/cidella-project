using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedEvent : MonoBehaviour
{
    public event Action<DestroyedEvent, DestroyedEventArgs> OnDestroyed;

    public void CallDestroyedEvent(bool isDead)
    {
        OnDestroyed?.Invoke(this, new DestroyedEventArgs { isDead = isDead});
    }
}

public class DestroyedEventArgs : EventArgs
{
    public bool isDead;
}
