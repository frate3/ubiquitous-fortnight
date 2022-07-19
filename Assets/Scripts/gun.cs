using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{

    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject player;
    GameObject bullet;
    [SerializeField] GameObject shootPoint;
    // Start is called before the first frame update
    void Start()
    {
        bullet = Resources.Load("bullet") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        fire();
        
    }

    void fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            GameObject newBullet = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
            bulletCode bulletScript = newBullet.GetComponent<bulletCode>();
            bulletScript.Init(player, crosshair, shootPoint);
            Destroy(newBullet, 10f);
        }
    }
}
