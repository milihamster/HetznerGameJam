using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Controls))]
public abstract class Animal : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;
    protected SpriteRenderer _spriteRenderer;

    protected Controls _controls;

    protected UnityEvent _onDeath;

    protected float _size = 1;

    public bool IsGrounded;

    void Start()
    {
        _controls = GetComponent<Controls>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Flip if Animal is heading the other way
        if (_rigidbody.velocity.x < 0)
            transform.localScale = Vector3.one * _size;
        else if (_rigidbody.velocity.x > 0)
            transform.localScale = new Vector3(-_size, _size, _size);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
            IsGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
            IsGrounded = false;
    }

}
