using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalGround : Animal
{
    [SerializeField]
    private float _maxSpeed = 3.5f;

    void LateUpdate()
    {
        // Reset speed to _maxSpeed if the Rigidbody is moving too fast
        if (_rigidbody.velocity.magnitude > _maxSpeed)
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;


        // Jump if animal is on ground
        if (_controls.Special && IsGrounded)
        {
            print("On Ground and Jumping!");
            _rigidbody.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        _rigidbody.AddForce(new(_controls.MovementHorizontal * 20, 0));
    }
}

