using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsAiGround : Controls
{
    public Vector3 _targetPosition;

    [SerializeField]
    private float _startCooldown = 10;
    [SerializeField]
    private float _range = 3;
    [SerializeField]
    private float _speed = 0.25f;
    private float _positionCooldown;

    private void LateUpdate()
    {
        Debug.DrawLine(transform.position, _targetPosition);
    }

    protected override void HandleControls()
    {
        if (_positionCooldown <= 0)
        {
            _targetPosition = transform.position + new Vector3(
            Random.Range(-_range, _range),
            transform.position.y, 0);

            _positionCooldown = Random.Range(0, _startCooldown);
        }
        else
            _positionCooldown -= Time.deltaTime;

        MovementHorizontal = _targetPosition.x - transform.position.x;

        if (MovementHorizontal > _speed)
            MovementHorizontal = _speed;
        else if (MovementHorizontal < -_speed)
            MovementHorizontal = -_speed;
    }
}
