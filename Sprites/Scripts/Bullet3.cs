using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet3: MonoBehaviour
{
    public float speed = 1000;
    private Rigidbody2D _rigidbody;
    public float maxLifetime = 5.0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

    
}