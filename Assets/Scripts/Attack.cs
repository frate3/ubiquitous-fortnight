using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public bool isTouching;
    public Animator animator;
    [SerializeField] moveemnt move;

    Collider[] col;

    private void Update()
    {
        //DamageStealer();
    }

    void OnTriggerEnter(Collider col)
    {
        /*col = Physics.OverlapSphere(move.transform.position, 15, LayerMask.GetMask("Player"));*/
        
        if (col.CompareTag("Player"))
        {
            /*Debug.Log("is attacking");*/
            animator.SetBool("Walking", false);
            animator.SetBool("Attacking", true);
            Invoke("reset",1);
        }
        else
        {
            isTouching = false;
        }
    }

    void reset(){
        animator.SetBool("Attacking", false);
    }
}
