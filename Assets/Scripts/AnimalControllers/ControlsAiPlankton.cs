using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimalPlankton))]
public class ControlsAiPlankton : ControlsAi
{
    public Vector3 _targetPosition;

    [SerializeField]
    private float _startCooldown = 10;
    [SerializeField]
    private float _range = 3;
    [SerializeField]
    private float _speed = 0.2f;
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
            Random.Range(-_range, _range), 0);

            // Check if there's something between me and the random position
            // Regenerate if that's the case
            var hits = Physics2D.RaycastAll(transform.position, _targetPosition, _range, LayerMask.GetMask("World"));
            if (hits.Length <= 1)
                _positionCooldown = Random.Range(0, _startCooldown);
        }
        else
            _positionCooldown -= Time.deltaTime;

        MovementVertical = _targetPosition.y - transform.position.y;
        MovementHorizontal = _targetPosition.x - transform.position.x;

        if (MovementHorizontal > _speed)
            MovementHorizontal = _speed;
        else if (MovementHorizontal < -_speed)
            MovementHorizontal = -_speed;

        if (MovementVertical > _speed)
            MovementVertical = _speed;
        else if (MovementVertical < -_speed)
            MovementVertical = -_speed;

    }

}
