using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthEvent))]
[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    public int startingHealth;
    public int currentHealth;

    [SerializeField] private bool isPlayer;
    private bool isDamageable = true;

    private bool isImmuneAfterHit;
    [SerializeField] private int immunityTime = 3;

    private Player player;
    private HealthEvent healthEvent;
    private SpriteRenderer sr;

    private void Awake()
    {
        if (isPlayer)
        {
            isImmuneAfterHit = true;
            player = GetComponent<Player>();
            sr = GetComponent<SpriteRenderer>();
        }
       healthEvent = GetComponent<HealthEvent>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
        isDamageable = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            TakeDamage(1);
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDamageable)
        {
            currentHealth -= damageAmount;
            CallHealthEvent(damageAmount);
            if (isImmuneAfterHit)
            {
                StartCoroutine(ImmunityRoutine());
            }
        }
    }

    private IEnumerator ImmunityRoutine()
    {
        isDamageable = false;

        while (immunityTime > 0)
        {
            sr.color = new(sr.color.r, sr.color.g, sr.color.b, .5f);
            yield return new WaitForSeconds(.4f);
            sr.color = new(sr.color.r, sr.color.g, sr.color.b, 1f);
            yield return new WaitForSeconds(.4f);
            immunityTime--;
            yield return null;
        }
        isDamageable = true;
    }

    public void AddHealth(int healthAmount)
    {
        currentHealth += healthAmount;
        
        if (currentHealth >= startingHealth)
        {
            currentHealth = startingHealth;
        }

        CallHealthEvent(0);
    }

    private void CallHealthEvent(int damageAmount)
    {
        healthEvent.CallHealthChangedEvent(currentHealth, damageAmount);
    }

}
