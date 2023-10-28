using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Controls))]
public abstract class Animal : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;

    protected Controls _controls;

    protected UnityEvent _onDeath;

    protected float _size = 1;

    // Start is called before the first frame update
    void Start()
    {
        _controls = GetComponent<Controls>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Flip if Animal is heading the other way
        if (_rigidbody.velocity.x < 0)
            transform.localScale = Vector3.one * _size;
        else if (_rigidbody.velocity.x > 0)
            transform.localScale = new Vector3(-_size, _size, _size);
    }

}
