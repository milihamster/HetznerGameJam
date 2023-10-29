using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlsAiGround : ControlsAi
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
                var lowerLevelAnimal = lowerLevelAnimals.FirstOrDefault(x => x != null && 
                x.AnimalSo.Level < _animal.AnimalSo.Level &&
                (x.AnimalSo.Type == AnimalType.LAND || x.AnimalSo.Type == AnimalType.SKY));
                if (lowerLevelAnimal != null)
                {
                    _attackingAnimal = lowerLevelAnimal;
                    _positionCooldown = Random.Range(0, _startCooldown);
                }
            }
            else
            {
                _attackingAnimal = null;

                float x = (Random.Range(0, 2) * 2 - 1) * Random.Range(0.1f * _range, _range);
                _targetPosition = transform.position + Vector3.right * x;
            }

            _positionCooldown = Random.Range(0.1f * _startCooldown, _startCooldown);
        }
        else
            _positionCooldown -= Time.deltaTime;

        if (_attackingAnimal != null)
            _targetPosition = _attackingAnimal.transform.position;

        MovementHorizontal = _targetPosition.x - transform.position.x;

        Attack = _attackingAnimal != null;

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
