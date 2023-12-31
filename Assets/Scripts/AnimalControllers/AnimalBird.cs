using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBird : Animal
{
    [SerializeField]
    private float _maxSpeed = 3;
    [SerializeField]
    private bool _wontDrown = false;

    void LateUpdate()
    {
        // Reset speed to _maxSpeed if the Rigidbody is moving too fast
        if (_rigidbody.velocity.magnitude > _maxSpeed)
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;

        Animator?.SetBool("Standing", IsGrounded);

        // Kill if drowning
        if (transform.position.y < -1 && !_wontDrown)
            Kill();
    }

    void FixedUpdate()
    {
        if (!IsGrounded)
            _rigidbody.AddForce(new(_controls.MovementHorizontal * 50, _controls.MovementVertical * 50));
        else
            _rigidbody.AddForce(new(0, _controls.MovementVertical * 20));
    }
}
