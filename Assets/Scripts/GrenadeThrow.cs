using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{

    public GameObject Grenade;
    [SerializeField] GameObject shootPoint;
    public GameObject explosion;
    public LayerMask whatIsEnemies;
    GameObject newGrenade;
    public Rigidbody rb;


    void Update(){
        if (Input.GetButtonDown("Throw")){
            newGrenade = Instantiate(Grenade, shootPoint.transform.position, shootPoint.transform.rotation);
            Invoke("Explode",3f);
            Destroy(newGrenade, 3f);
        }
    }

    void Explode(){
        GameObject explode = Instantiate(explosion, newGrenade.transform.position, Quaternion.identity);
        explode.transform.position = newGrenade.transform.position;


        Collider[] enemies = Physics.OverlapSphere(transform.position, 10f, whatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            print(enemies.Length);
            enemies[i].GetComponent<Enemy>().hit();
            //Add explosion force (if enemy has a rigidbody)
        }
        Destroy(explode, 1f);

        
    }
   


    
}
