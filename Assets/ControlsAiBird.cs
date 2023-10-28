using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsAiBird : Controls
{
    public Vector3 _targetPosition;

    [SerializeField]
    private float _startCooldown = 10;
    [SerializeField]
    private float _range = 3;
    [SerializeField]
    private float _speed = 0.25f;
    [SerializeField]
    private float _maxHeight = 25;
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

            // Stop birds from going too far below water level
            // If they hit the ground, they'll stay there for a while
            // since birds can't move when grounded
            if (_targetPosition.y < -2.5f)
                _targetPosition += new Vector3(0, -_targetPosition.y, 0);
            else if (_targetPosition.y > _maxHeight)
                _targetPosition += new Vector3(0, _maxHeight - _targetPosition.y, 0);

            // Check if there's something between me and the random position
            // Regenerate if that's the case
            var hits = Physics2D.RaycastAll(transform.position, _targetPosition, _range, LayerMask.GetMask("World"));
            if (hits.Length <= 1)
                _positionCooldown = Random.Range(0, _startCooldown);
            else
                print($"Found {hits.Length} hits");
        }
        else if (Vector3.Distance(_targetPosition, transform.position) < 0.25f)
            _positionCooldown = 0;
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
