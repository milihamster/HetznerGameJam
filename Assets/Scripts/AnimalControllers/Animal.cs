using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controls))]
public abstract class Animal : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;

    protected Controls _controls;

    // Start is called before the first frame update
    void Start()
    {
        _controls = GetComponent<Controls>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

}
