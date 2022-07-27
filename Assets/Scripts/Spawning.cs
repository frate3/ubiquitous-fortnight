using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Spawning : MonoBehaviour
{

    bool spawning = false;
    public GameObject enemy;
    public Transform spawnerSpot;
    int wave;

    private void Start()
    {
        Spawn();
    }

    private void Spawn(){
        float spawnAmount = SpawnNum();
        wave++;
        for (int i=0; i < spawnAmount; i++){
            GameObject newEnemy = Instantiate(enemy, randomVector3(spawnerSpot, -10, 10), Quaternion.identity);
        }
        Invoke("Spawn",15);
        
    }

    public float SpawnNum()
    {
        return Random.Range(5, 10) * wave/3; //add more factors later
    }
    
    Vector3 randomVector3 (Transform host, float min, float max)
    {
        Vector3 basePosition = host.position;
        float offsetX = Random.Range(min, max);
        float offsetZ = Random.Range(min, max);
        basePosition += new Vector3(offsetX, 0, offsetZ);
        return basePosition;

    }
    
}
