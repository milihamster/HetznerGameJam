using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBeetle : Animal
{
    [SerializeField]
    private float _maxSpeed = 0.1f;

    void LateUpdate()
    {
        // Reset speed to _maxSpeed if the Rigidbody is moving too fast
        if (_rigidbody.velocity.magnitude > _maxSpeed)
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
    }

    void FixedUpdate()
    {
        _rigidbody.AddForce(new(_controls.MovementHorizontal * 2, _controls.MovementVertical * 2));

    }
}
