using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPlankton : Animal
{
    [SerializeField]
    private float _speed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        _rigidbody.velocity = new Vector2(0.0f, 1.0f) * _speed;
    }
}
