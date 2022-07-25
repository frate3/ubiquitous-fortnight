using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCode : MonoBehaviour
{
    RaycastHit hit;
    Rigidbody rb;
    float force = 20f;
    GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
        
        
    }

    

    public void Init(GameObject player, GameObject gun)
    {
        if (player != null) {


            Ray r = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            Physics.Raycast(r, out hit, Mathf.Infinity);
            Vector3 direction = new Vector3();

            if (hit.collider != null)
            {
                
                direction = hit.point - gun.transform.position;
            }
            else
            {
                
                direction = r.GetPoint(50) - gun.transform.position;
            }

            
            //Debug.DrawRay(gun.transform.position, direction, Color.red, 15f);
            rb.AddForce(direction.normalized * force, ForceMode.Impulse);
        }

        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Instantiate(); 
        Destroy(gameObject);
    }




}
