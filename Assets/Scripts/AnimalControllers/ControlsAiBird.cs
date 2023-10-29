using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlsAiBird : ControlsAi
{
    public Vector3? ForceTargetPosition;
    public Vector3 TargetPosition;

    [SerializeField]
    private float _startCooldown = 10;
    [SerializeField]
    private float _range = 3;
    [SerializeField]
    private float _speed = 0.25f;
    [SerializeField]
    private float _maxHeight = 25;
    private float _positionCooldown;

    private Animal _attackingAnimal;

    private void LateUpdate()
    {
        Debug.DrawLine(transform.position, TargetPosition);
    }

    protected override void HandleControls()
    {
        if (_positionCooldown <= 0)
        {
            int shouldIAttack = Random.Range(0, 2);
            if (AllowAttack && shouldIAttack == 1 && _attackingAnimal == null)
            {
                // Try looking for lower level animal to attack
                var surroundings = Physics2D.CircleCastAll(transform.position, _range / 2, TargetPosition, LayerMask.GetMask("Animals"));
                var lowerLevelAnimals = surroundings.Select(x => x.collider.gameObject.GetComponent<Animal>());
                var lowerLevelAnimal = lowerLevelAnimals.FirstOrDefault(x => x != null && x.AnimalSo?.Level < _animal.AnimalSo?.Level);
                if (lowerLevelAnimal != null)
                {
                    _attackingAnimal = lowerLevelAnimal;
                    _positionCooldown = Random.Range(0, _startCooldown);
                }
            }
            else
            {
                _attackingAnimal = null;

                TargetPosition = transform.position + new Vector3(
                    Random.Range(-_range, _range),
                    Random.Range(-_range, _range), 0);

                // Stop birds from going too far below water level
                // If they hit the ground, they'll stay there for a while
                // since birds can't move when grounded
                if (TargetPosition.y < -2.5f)
                    TargetPosition += new Vector3(0, -TargetPosition.y, 0);
                else if (TargetPosition.y > _maxHeight)
                    TargetPosition += new Vector3(0, _maxHeight - TargetPosition.y, 0);

                // Check if there's something between me and the random position
                // Regenerate if that's the case
                var hits = Physics2D.RaycastAll(transform.position, TargetPosition, _range, LayerMask.GetMask("World"));
                if (hits.Length <= 1)
                    _positionCooldown = Random.Range(0, _startCooldown);
            }
        }
        else if (Vector3.Distance(TargetPosition, transform.position) < 0.25f)
            _positionCooldown = 0;
        else
            _positionCooldown -= Time.deltaTime;

        if (_attackingAnimal != null)
            TargetPosition = _attackingAnimal.transform.position;

        if (ForceTargetPosition != null)
            TargetPosition = ForceTargetPosition.Value;

        MovementVertical = TargetPosition.y - transform.position.y;
        MovementHorizontal = TargetPosition.x - transform.position.x;

        Attack = _attackingAnimal != null && ForceTargetPosition == null;

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
