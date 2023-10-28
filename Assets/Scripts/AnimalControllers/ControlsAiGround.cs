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
            float x = (Random.Range(0, 2) * 2 - 1) * Random.Range(0.1f * _range, _range);
            _targetPosition = transform.position + Vector3.right * x;

            _positionCooldown = Random.Range(0.1f * _startCooldown, _startCooldown);
        }
        else
            _positionCooldown -= Time.deltaTime;

        MovementHorizontal = _targetPosition.x - transform.position.x;

        if (Mathf.Abs(MovementHorizontal) < 0.1f)
        {
            MovementHorizontal = 0;
        }

        if (Mathf.Abs(MovementHorizontal) > _speed)
        {
            MovementHorizontal = Mathf.Sign(MovementHorizontal) * _speed;
        }
    }
}
