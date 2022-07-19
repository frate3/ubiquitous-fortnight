using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gun : MonoBehaviour
{

    [SerializeField] Text ammoCounter;
    float bulletsLeft;
    /*public float bulletsFired;*/
    float mag = 40;
    float maxMag;
    float timeBetweenReload = 1;
    bool reload = false;
    float timeBetweenShoot = 0.2f;
    bool readyToShoot = true;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject player;
    GameObject bullet;
    [SerializeField] GameObject shootPoint;
    // Start is called before the first frame update
    void Start()
    {

        maxMag = mag;
        bullet = Resources.Load("bullet") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ammoCounter.text = mag.ToString("0");
        fire();


    }

    void fire()
    {
        if (mag < 40 && Input.GetKeyDown(KeyCode.R))
        {
            reload = true;
            Invoke("reloadCheck", timeBetweenReload);
        }
        if (Input.GetMouseButton(0))
        {

            if (!reload && readyToShoot && mag > 0)
            {
                readyToShoot = false;
                GameObject newBullet = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
                mag--;

                bulletCode bulletScript = newBullet.GetComponent<bulletCode>();
                bulletScript.Init(player, shootPoint);
                Destroy(newBullet, 10f);
                Invoke("reset", timeBetweenShoot);
            }

        }
    }

    void reloadCheck()
    {
        reload = false;
        mag = maxMag;
    }

    void debug()
    {
        Debug.Log("This is mag " + mag);
        Debug.Log("This is reload" + reload);
    }

    private void reset()
    {
        readyToShoot = true;
    }
}
