using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_Damage : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        Controller_Ruby player = other.gameObject.GetComponent<Controller_Ruby>();

        if (player != null)
        {
            //NO SÉ QUE ES LO QUE FALLA AQUÍ
            //player.ChangeHealth(-1);
        }    
    }
}
