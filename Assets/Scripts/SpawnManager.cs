using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; } 

    public GameObject plancton;
    public GameObject seaCreature1;
    public GameObject seaCreature2;
    public GameObject landCreature1;
    public GameObject landCreature2;
    public GameObject skyCreature1;
    public GameObject skyCreature2;

    public Transform spawnAreaSea;
    public Transform spawnAreaLand;
    public Transform spawnAreaSky;

    public int enemyMaxCount;
    public int planctonSpawnAmount;
    public float planctonSpawnRate;

    public SpawnManager()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        RespawnPlayer();
        InitializeEnemies();
        InvokeRepeating("SpawnPlanctons", 0, planctonSpawnRate);
    }

    public void RespawnPlayer()
    {
        SpawnObject(seaCreature1, spawnAreaSea, false);
    }

    public void ReSpawnEnemy(GameObject enemy)
    {
        Transform chosenArea;
        Animal animalComponent = enemy.GetComponent<Animal>();
        bool grounded = false;

        switch (animalComponent.animalType)
        {
            case AnimalType.SeaCreature1:
                chosenArea = spawnAreaSea;
                break;
            case AnimalType.SeaCreature2:
                chosenArea = spawnAreaSea;
                break;
            case AnimalType.LandCreature1:
                grounded = true;
                chosenArea = spawnAreaLand;
                break;
            case AnimalType.LandCreature2:
                grounded = true;
                chosenArea = spawnAreaLand;
                break;
            case AnimalType.SkyCreature1:
                chosenArea = spawnAreaSky;
                break;
            case AnimalType.SkyCreature2:
                chosenArea = spawnAreaSky;
                break;
            default:
                throw new Exception("Missing enemy type");
        }

        SpawnObject(enemy, chosenArea, grounded);
    }

    public void LevelUp(GameObject target) 
    {
        Animal animalComponent = target.GetComponent<Animal>();

        GameObject newForm = target.gameObject;
        Vector3 spawnPosition = target.transform.position;

        switch (animalComponent.animalType)
        {
            case AnimalType.SeaCreature1:
                newForm = seaCreature2;
                break;
            case AnimalType.SeaCreature2:
                newForm = landCreature1;
                break;
            case AnimalType.LandCreature1:
                newForm = landCreature2;
                RaycastHit hit;
                if (Physics.Raycast(spawnPosition, -Vector3.up, out hit))
                {
                    spawnPosition.y = hit.point.y;
                }
                break;
            case AnimalType.LandCreature2:
                newForm = skyCreature1;
                break;
            case AnimalType.SkyCreature1:
                newForm = skyCreature2;
                break;
            case AnimalType.SkyCreature2:
                break;
            default:
                throw new Exception("Missing enemy type");
        }

        //TODO: LevelUpAnimation
        Instantiate(newForm, spawnPosition, target.transform.rotation);
        Destroy(target);
    }

    void InitializeEnemies()
    {
        for (int i = 0; i < enemyMaxCount; i++)
        {
            SpawnObject(seaCreature1, spawnAreaSea, false);
        }
    }

    void SpawnPlanctons()
    {
        for (int i = 0; i < planctonSpawnAmount; i++)
        {
            SpawnObject(plancton, spawnAreaSea, true);
        }
    }

    void SpawnObject(GameObject obj, Transform area, bool grounded)
    {
        // Bereich
        float randomX = UnityEngine.Random.Range(0, area.localScale.x);
        float randomY = UnityEngine.Random.Range(0, area.localScale.y);
        Vector3 spawnPoint = new Vector3(randomX, randomY);
        if(grounded)
        {
            RaycastHit hit;
            if(Physics.Raycast(spawnPoint, -Vector3.up, out hit))
            {
                spawnPoint.y = hit.point.y;
            }            
        }
        Instantiate(obj, spawnPoint, obj.transform.rotation);
    }
}
