using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gun : MonoBehaviour
{

    float currentTime = 0f;
    float timeToMove = .5f;
    bool canReloadAnim = false;
    [SerializeField] Text ammoCounter;
    float bulletsLeft;
    /*public float bulletsFired;*/
    float mag = 40;
    float maxMag;
    float timeBetweenReload = 2;
    bool reload = false;
    float timeBetweenShoot = 0.2f;
    bool readyToShoot = true;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject player;
    GameObject bullet;
    [SerializeField] GameObject shootPoint;
    Vector3 startPosition;
    Vector3 lowerPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
        maxMag = mag;
        bullet = Resources.Load("bullet") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ammoCounter.text = mag.ToString("0");
        fire();
        reloadAnim();
        lowerPosition = transform.localPosition - new Vector3(0, 3, 0);


    }

    void fire()
    {
        if (mag < 40 && Input.GetKeyDown(KeyCode.R))
        {
            canReloadAnim = true;
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

    void reloadAnim()
    {
        if (canReloadAnim)
        {
            if (currentTime <= timeToMove)
            {
                currentTime += Time.deltaTime;
                
                transform.localPosition = Vector3.Lerp(startPosition, lowerPosition, currentTime / timeToMove);
                
            }
            else
            {
                canReloadAnim = false;
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
