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
    


    private void OnTriggerEnter(Collider col){
        if (!spawning){
            spawning = true;
            Spawn();
        }
    }

    private void Spawn(){
        float spawnAmount = SpawnNum();
        
        for (int i=0; i<spawnAmount; i++){
            GameObject newEnemy = Instantiate(enemy, spawnerSpot);
        }
        
        
    }

    public float SpawnNum(){
        return Random.Range(5,10); //add more factors later
    } 

    

    
}
