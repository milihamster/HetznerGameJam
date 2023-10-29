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
            if(Vector2.Distance(_spotOverSea.transform.position, _playerAnimal.transform.position) < 1)
            {
                // Change Sprite here
                // drop new Animal

                SpawnManager.Instance.RespawnPlayer(_playerAnimal.transform.position += new Vector3(-0.6f, -0.3125f, 0));
                _cutsceneActive = false;
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

            _cutsceneActive = true;
        }
    }
}
