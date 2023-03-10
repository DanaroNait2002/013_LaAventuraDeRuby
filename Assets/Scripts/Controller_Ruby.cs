using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Ruby : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField]
    float speed = 3.0f;

    [Header("Rigidbody")]
    [SerializeField]
    Rigidbody2D rigidbody2d;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = rigidbody2d.position;

        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        transform.position = position;

        rigidbody2d.MovePosition(position);
    }
}
