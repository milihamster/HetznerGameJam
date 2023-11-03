using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NestTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform _spotOverSea;

    private Animal _playerAnimal;
    private bool _cutsceneActive;

    void Update()
    {
        if(_cutsceneActive)
        {
            _playerAnimal.Attack();

            if(Vector2.Distance(_spotOverSea.transform.position, _playerAnimal.transform.position) < 1)
            {
                SpawnManager.Instance.RespawnPlayer(_playerAnimal.AttackTrigger.transform.position);
                _cutsceneActive = false;

                _playerAnimal.Animator?.SetBool("Final", true);
                LeanTween.value(0, 1, 4f)
                    .setOnComplete(() => {
                        _playerAnimal.Animator?.SetBool("Final", false);
                        _playerAnimal.AttackAlwaysSucceeds = false;
                    } );
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<ControlsPlayer>(out var playerControls) &&
            playerControls.enabled && 
            playerControls.GetComponent<Animal>().AnimalSo.Level == 
                GlobalDataSo.Instance.Animals.Max(x => x.Level))
        {
            playerControls.enabled = false;
            // player should only get here with a bird
            var aiControls = playerControls.GetComponent<ControlsAiBird>();
            aiControls.enabled = true;
            aiControls.ForceTargetPosition = _spotOverSea.position;

            var animal = playerControls.GetComponent<Animal>();
            animal.SetControls(aiControls);
            _playerAnimal = animal;

            CameraController.Instance.SetCameraSize(15);

            _playerAnimal.AttackAlwaysSucceeds = true;

            _cutsceneActive = true;
        }
    }
}
