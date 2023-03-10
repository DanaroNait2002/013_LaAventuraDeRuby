using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Collectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Health_Ruby controller = other.GetComponent<Health_Ruby>();

        if (controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
