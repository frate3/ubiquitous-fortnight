using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    NavMeshAgent agent;
    int count;
    Animator animator;
    bool walking;

    // Start is called before the first frame update
    void Awake(){
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        walking = true;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Check();
        print("count" + count);
        if (walking){
            animator.SetBool("Walking", true);
            agent.destination = player.transform.position;
        } else {
            animator.SetBool("Walking", false);
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            count++;
        }
    }

    private void Die () {
        
        Destroy(gameObject);
    }

    void Check(){
        

        if (count >= 5){
            animator.SetBool("Dead", true);
            animator.SetBool("Attacking", false);
            walking = false;

            Invoke("Die", 1.15f); 
        }
    }
}
