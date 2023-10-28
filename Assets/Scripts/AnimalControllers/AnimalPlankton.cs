using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPlankton : Animal
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
        _rigidbody.AddForce(-Physics.gravity, ForceMode2D.Force);  // Counter Gravity
        _rigidbody.AddForce(new(_controls.MovementHorizontal * 2, _controls.MovementVertical * 2));

        // If above water
        if (transform.position.y > 0)
            _rigidbody.AddForce(new(0, -50));

        if (transform.position.y + 2 * (_collider as CircleCollider2D).radius > 0)
            _rigidbody.AddForce(new(0, -1));
    }
}
