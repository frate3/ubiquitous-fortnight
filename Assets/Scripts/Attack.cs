using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public Animator animator;
    [SerializeField] moveemnt move;

    

    private void OnTriggerStay(Collider col)
    {
        print(col.tag);
        if (col.CompareTag("Player"))
        {
            Debug.Log("is attacking");
            Damage.TakeDamage(ref move.health, 1);
            animator.SetBool("Walking", false);
            animator.SetBool("Attacking", true);
            Invoke("reset",1);
        }
    }

    void reset(){
        animator.SetBool("Attacking", false);
    }
}
