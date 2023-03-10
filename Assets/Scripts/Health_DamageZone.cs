using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_DamageZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Health_Ruby controller = other.GetComponent<Health_Ruby>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}
