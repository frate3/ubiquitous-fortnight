using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] Spawning spawn;
    public void hit(){

        spawn.spiders.Remove(gameObject);
        Destroy(gameObject);
    }

}
