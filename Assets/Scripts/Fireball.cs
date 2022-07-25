using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Instantiate();
        if (other.CompareTag("Player"))
        {
            moveemnt.TakeDamage(10);
        }


        if (other.tag != "hit")
        {
            Destroy(gameObject);
        }
        
    }




}
