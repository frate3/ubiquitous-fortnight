using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    public Animator animator;
    public bool active= false;
    float fireballForce = 4;
    public Transform target;
    float attackCoolDown = 5;
    public GameObject fireBall;
    [SerializeField] GameObject attackPoint;
    public bool attacking = false;
    bool coolDown = true;
    int attacksRemaining;
    int maxattacks = 3;
    bool cool;
    [SerializeField] GameObject player;
    int count = 0;


    private void Update()
    {
        if (!active)
        {
            animator.SetBool("Sleep", true);
            Collider[] contacts = Physics.OverlapSphere(transform.position, 30f);
            for (int i = 0; i < contacts.Length; i++)
            {
                if (contacts[i].tag == "Player")
                {
                    active = true;
                    transform.LookAt(target);
                    animator.SetBool("Sleep", false);
                    attacking = true;
                }

            }
        }

        if (attacking)
        {
            attacking = false;
            attacksRemaining = 3;
            animator.SetInteger("Attacks", attacksRemaining);
            
            Invoke("Attack", 3f);
            
        }

        if (count >= 60)
        {
            animator.SetBool("dead", true);
            Destroy(gameObject, 1.5f);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            count++;
        }
    }


    void Attack()
    {
        transform.LookAt(target);
        attacksRemaining--;
        animator.SetInteger("Attacks", attacksRemaining);
        GameObject newFireball = Instantiate(fireBall, attackPoint.transform.position, attackPoint.transform.rotation);
        newFireball.GetComponent<Rigidbody>().AddForce((player.transform.position - attackPoint.transform.position).normalized * fireballForce, ForceMode.Impulse);

        if (attacksRemaining > 0)
        {
            Invoke("Attack", 1f);
        } else {
            Invoke("reset", attackCoolDown);
        }
        transform.LookAt(target);
        
    }


    void reset()
    {
        attacking = true;
    }
}
