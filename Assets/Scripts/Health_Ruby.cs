using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Ruby : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    public int maxHealth = 5;
    [SerializeField]
    public int health { get { return currentHealth; } }
    [SerializeField]
    int currentHealth;

    [Header("Invencible")]
    [SerializeField]
    float timeInvincible = 2.0f;
    [SerializeField]
    bool isInvincible;
    [SerializeField]
    float invincibleTimer;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
