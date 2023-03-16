using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_Movement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField]
    float speed = 3.0f;

    [Header("Direction")]
    [SerializeField]
    bool vertical;
    [SerializeField]
    int direction = 1;

    [Header("Time")]
    [SerializeField]
    float changeTime = 3.0f;
    [SerializeField]
    float timer;

    [Header("Rigidbody")]
    [SerializeField]
    Rigidbody2D Rigidbody2D;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        Vector2 position = Rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed;
        }

        Rigidbody2D.MovePosition(position);
    }
}
