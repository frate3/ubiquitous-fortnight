using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    private Animator animator;
    public Transform enemy;
    public Vector3 lastPos;

    public float health = 100;


    private void Awake(){
        animator = GetComponent<Animator>();
    }

    private void Update(){
        if (enemy.transform.position.x != lastPos.x){
            animator.SetBool("Walking", true);
        } else {
            animator.SetBool("Walking", false);
        }
        if (health <= 0){
            animator.SetBool("Dead", true);
            //Destroy
        }
        
    // if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")){
    //     animator.SetBool("Walking", false);
    // }
    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1")){
        animator.SetBool("Attacking", false);
    }


    lastPos = enemy.transform.position;
    }   

    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Player"){
            animator.SetBool("Attacking", true);
        }
        if (col.gameObject.tag == "Weapon"){
            health -= 20;
        }
    }
}


