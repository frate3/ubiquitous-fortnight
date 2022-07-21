using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{

    public GameObject Grenade;
    [SerializeField] GameObject shootPoint;

    void Update(){
        if (Input.GetButtonDown("Throw")){
            GameObject newGrenade = Instantiate(Grenade, shootPoint.transform.position, shootPoint.transform.rotation);
        }
    }
}
