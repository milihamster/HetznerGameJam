using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AnimalFish))]
public class ControlsAiFish : ControlsAi
{
    public Vector3 _targetPosition;

    [SerializeField]
    private float _startCooldown = 10;
    [SerializeField]
    private float _range = 3;
    [SerializeField]
    private float _speed = 0.25f;
    private float _positionCooldown;

    private Animal _attackingAnimal;

    private void LateUpdate()
    {
        Debug.DrawLine(transform.position, _targetPosition);
    }

    protected override void HandleControls()
    {
        if (_positionCooldown <= 0)
        {
            int shouldIAttack = Random.Range(0, 2);
            if (AllowAttack && shouldIAttack == 1 && _attackingAnimal == null)
            {
                // Try looking for lower level animal to attack
                var surroundings = Physics2D.CircleCastAll(transform.position, _range / 2, _targetPosition, LayerMask.GetMask("Animals"));
                var lowerLevelAnimals = surroundings.Select(x => x.collider.gameObject.GetComponent<Animal>());
                var lowerLevelAnimal = lowerLevelAnimals.FirstOrDefault(x => x != null && x.AnimalSo?.Level < _animal.AnimalSo?.Level);
                if (lowerLevelAnimal != null)
                {
                    _attackingAnimal = lowerLevelAnimal;
                    _positionCooldown = Random.Range(0, _startCooldown);
                    _animal.PlayAttackParticles();
                }
            }
            else
            {
                _attackingAnimal = null;
                _animal.StopAttackParticles();

                _targetPosition = transform.position + new Vector3(
                Random.Range(-_range, _range),
                Random.Range(-_range, _range), 0);

                // Check if there's something between me and the random position
                // Regenerate if that's the case
                var hits = Physics2D.RaycastAll(transform.position, _targetPosition, _range, LayerMask.GetMask("World"));
                if (hits.Length <= 1)
                    _positionCooldown = Random.Range(0, _startCooldown);
            }
        }
        else
            _positionCooldown -= Time.deltaTime;

        if (_attackingAnimal != null)
            _targetPosition = _attackingAnimal.transform.position;

        MovementVertical = _targetPosition.y - transform.position.y;
        MovementHorizontal = _targetPosition.x - transform.position.x;

        Attack = _attackingAnimal != null;

        if (Mathf.Abs(MovementHorizontal) < 0.1f)
        {
            MovementHorizontal = 0;
        }

        if (MovementHorizontal > _speed)
            MovementHorizontal = _speed;
        else if (MovementHorizontal < -_speed)
            MovementHorizontal = -_speed;
    }
}
