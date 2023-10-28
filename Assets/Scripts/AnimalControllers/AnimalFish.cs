using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFish : Animal
{
    [SerializeField]
    private float _maxSpeed = 2;

    void LateUpdate()
    {
        // Reset speed to _maxSpeed if the Rigidbody is moving too fast
        if (_rigidbody.velocity.magnitude > _maxSpeed)
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;

        // Flip if Animal is heading the other way
        if (_rigidbody.velocity.x < 0)
            _spriteRenderer.flipX = false;
        else if (_rigidbody.velocity.x > 0)
            _spriteRenderer.flipX = true;

        _onDeath.AddListener(() => OnDeath());
    }

    void FixedUpdate()
    {
        _rigidbody.AddForce(new(_controls.MovementHorizontal * 50, _controls.MovementVertical * 50));

        // If above water
        if (transform.position.y > 0)
            _rigidbody.AddForce(new(0, -50));
    }

    void OnDeath()
    {
        Destroy(gameObject);
        SpawnManager.Instance.RespawnPlayer();
    }
}
