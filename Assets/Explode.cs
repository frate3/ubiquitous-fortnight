using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public ParticleSystem explosion;
    
    public void playExplosion(){
        //play explosion
        explosion.Play();
    }
}
