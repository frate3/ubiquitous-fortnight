// using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gun : MonoBehaviour
{

    public ParticleSystem ps;
    public Animator animController;
    float currentTime = 0f;
    float timeToMove = .5f;
    bool canReloadAnim;
    [SerializeField] Text ammoCounter;
    float bulletsLeft;
    /*public float bulletsFired;*/
    float mag = 40;
    float maxMag;
    float timeBetweenReload = 2f;
    public bool reload = false;
    float timeBetweenShoot = 0.5f;
    public bool readyToShoot = true;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject player;
    [SerializeField] GameObject bulletHole;
    GameObject bullet;
    [SerializeField] GameObject shootPoint;

    // Start is called before the first frame update


    
    private void Awake()
    {
        //animController = GetComponent<Animator>();
    }
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
        //reloadAnim();


    }

    void fire()
    {
        if (mag < 40 && Input.GetKeyDown(KeyCode.R))
        {
            animController.SetBool("reload", true);
            reload = true;
            Invoke("reloadCheck", timeBetweenReload);
        }
        if (Input.GetMouseButton(0))
        {

            if (!reload && readyToShoot && mag > 0)
            {
                readyToShoot = false;
                Flash();
                GameObject newBullet = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
                mag--;
                bulletCode bulletScript = newBullet.GetComponent<bulletCode>();
                bulletScript.Init(player, shootPoint, bulletHole);
                Destroy(newBullet, 10f);
                Invoke("reset", timeBetweenShoot);
            }

        }
    }

    void Flash(){

        // ParticleSystem flash = Instantiate(ps, shootPoint.transform.position, shootPoint.transform.rotation);
    }


    void reloadCheck()
    {
        animController.SetBool("reload", false);
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
