using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public Animator animator;


    private void OnTriggerEnter(Collider col)
    {
        print(col.tag);
        if (col.CompareTag("Player"))
        {
            moveemnt.TakeDamage(1);
            animator.SetBool("Walking", false);
            animator.SetBool("Attacking", true);
            Invoke("reset",1);
        }
    }

    void reset(){
        animator.SetBool("Attacking", false);
    }
}
