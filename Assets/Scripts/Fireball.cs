using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] moveemnt move;
    private void Update()
    {
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Instantiate();
        if (other.CompareTag("Player"))
        {
            /*Damage.TakeDamage(move.health, 10);*/
        }


        if (other.tag != "hit")
        {
            Destroy(gameObject);
        }
        
    }




}
