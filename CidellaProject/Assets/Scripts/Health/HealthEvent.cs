using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEvent : MonoBehaviour
{
    public event Action<HealthEvent, HealthEventArgs> OnHealthChanged;
    
    public void CallHealthChangedEvent(int healthAmount, int damageAmount)
    {
        OnHealthChanged?.Invoke(this, new HealthEventArgs { healthAmount = healthAmount, damageAmount = damageAmount});
        Debug.Log(healthAmount);
    }
}

public class HealthEventArgs : EventArgs
{
    public int healthAmount;
    public int damageAmount;
}