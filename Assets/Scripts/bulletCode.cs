using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCode : MonoBehaviour
{
    RaycastHit hit;
    Rigidbody rb;
    float force = 12f;
    GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
        
        
    }

    private void Update()
    {
        if (rb != null)
        {
            Debug.Log("fax");
        }
    }

    public void Init(GameObject player, GameObject crosshair, GameObject gun)
    {
        if (player != null) {


            Ray r = new Ray(player.transform.position, player.transform.forward);

            Physics.Raycast(r, out hit, Mathf.Infinity);
            Vector3 direction = Vector3.zero;

            if (hit.collider != null)
            {
                print(hit.collider);
                direction = hit.point - gun.transform.position;
            }
            else
            {
                print(hit.collider);
                direction = r.GetPoint(50) - gun.transform.position;
            }

            direction.y = 0;
            rb.AddForce(direction.normalized * force, ForceMode.Impulse);
        }

        
        
    }




}
