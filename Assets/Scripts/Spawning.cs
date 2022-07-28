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
    public bool enemiesSpawned = true;
    int amount = WaveManager.startingAmount;
    bool enemiesDead = true;
    public List<GameObject> spiders;

    void Update()
    {

        spawn();
        Debug.Log(spiders.Count);
    }

    void spawn()
    {
        enemyCheck();
        if (enemiesDead)
        {
            spawnAmount();
        }
    }

    void enemyCheck()
    {
        if (spiders.Count <= 0)
        {
            Debug.Log("is fax");
            enemiesDead = true;
        }
        else if (spiders.Count > 0)
        {
            enemiesDead = false;
        }
    }
    

    IEnumerator spawnAmount()
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(5f);
            GameObject fart = Instantiate(enemy, WaveManager.randomVector3(spawnerSpot, -5, 5), Quaternion.identity);
            spiders.Add(fart);
        }
        amount += 5;
    }


}
