using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

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
        InitializeEnemies();
        InvokeRepeating("SpawnPlanctons", 0, planctonSpawnRate);
    }

    public void SpawnEnemy(EnemyType enemy)
    {
        // Bereich
        GameObject chosenEnemy;
        Transform chosenArea;
        bool grounded = false;

        switch (enemy)
        {
            case EnemyType.SeaCreature1:
                chosenEnemy = seaCreature1;
                chosenArea = spawnAreaSea;
                break;
            case EnemyType.SeaCreature2:
                chosenEnemy = seaCreature2;
                chosenArea = spawnAreaSea;
                break;
            case EnemyType.LandCreature1:
                chosenEnemy = landCreature1 ;
                grounded = true;
                chosenArea = spawnAreaLand;
                break;
            case EnemyType.LandCreature2:
                chosenEnemy = landCreature2;
                grounded = true;
                chosenArea = spawnAreaLand;
                break;
            case EnemyType.SkyCreature1:
                chosenEnemy = skyCreature1;
                chosenArea = spawnAreaSky;
                break;
            case EnemyType.SkyCreature2:
                chosenEnemy = skyCreature2;
                chosenArea = spawnAreaSky;
                break;
            default:
                throw new Exception("Missing enemy type");
        }

        SpawnObject(chosenEnemy, chosenArea, grounded);
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
            SpawnObject(seaCreature1, spawnAreaSea, true);
        }
    }

    void SpawnObject(GameObject obj, Transform area, bool grounded)
    {
        // Bereich
        Vector3 spawnPoint = new Vector3(area.localScale.x, area.localScale.y);
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
