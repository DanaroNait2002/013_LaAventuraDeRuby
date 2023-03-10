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

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
