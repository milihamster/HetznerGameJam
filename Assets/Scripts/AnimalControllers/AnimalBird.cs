using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBird : Animal
{
    [SerializeField]
    private float _maxSpeed = 3;

    void LateUpdate()
    {
        // Reset speed to _maxSpeed if the Rigidbody is moving too fast
        if (_rigidbody.velocity.magnitude > _maxSpeed)
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGrounded)
            _rigidbody.AddForce(new(_controls.MovementHorizontal * 10, _controls.MovementVertical * 20));
        else
            _rigidbody.AddForce(new(0, _controls.MovementVertical * 20));
    }
}
