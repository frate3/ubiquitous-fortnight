using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunController : MonoBehaviour
{
    bool canSwitch = false;
    [SerializeField] gun instance;
    [SerializeField] pistolScript instance1;
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject assaultRifle;
    [SerializeField] int mouse;
    List<GameObject> gunlist = new List<GameObject>();
    int startingWeapon = 1;
    GameObject currentWeapon;
    float timeForSwitch = 1;
    int lastMouse;
    float waitTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        addGuns();
        for (int i = 0; i < gunlist.Count; i++)
        {
            gunlist[i].SetActive(false);
            //Debug.Log(gunlist);
        }
        mouse = startingWeapon;
    }

    // Update is called once per frame
    void Update()
    {

        inputGetter();

        addTime();
        if (waitTime >= timeForSwitch && !instance.reload && !instance1.reload)
        {
            switchWeapon();
            print("can run");
            canSwitch = true;
        }

        else
        {
            canSwitch = false;
        }

        //weaponChange();
        /*Debug.Log(waitTime);*/


    }

    void weaponChange()
    {
        if (canSwitch)
        {
            if (!instance.reload && !instance1.reload)
            {
                for (int i = 0; i < gunlist.Count; i++)
                {
                    if (i == mouse)
                    {
                        gunlist[i].SetActive(true);

                    }
                    else
                    {
                        gunlist[i].SetActive(false);
                    }
                    if (mouse != lastMouse)
                    {
                        Debug.Log("fax");
                        Invoke("reset", timeForSwitch);
                    }
                }

            }
        }
    }

    void switchWeapon()
    {

        if (!instance.reload && !instance1.reload)
        {
            canSwitch = false;

            for (int i = 0; i < gunlist.Count; i++)
            {
                if (i == mouse)
                {

                    gunlist[i].SetActive(true);

                }

                else if (mouse != lastMouse)
                {


                    waitTime = 0;
                    StartCoroutine(anim(gunlist[lastMouse], i));
                    canSwitch = true;



                }
            }


        }

    }

    private void addTime()
    {

        waitTime += Time.deltaTime;
    }

    IEnumerator anim(GameObject gun, int num)
    {

        gun.GetComponent<Animator>().SetBool("switching", true);
        yield return new WaitForSeconds(1);
        gunlist[num].SetActive(false);


    }
    void addGuns()
    {
        gunlist.Add(pistol);
        gunlist.Add(assaultRifle);



    }

    void inputGetter()
    {

        if (canSwitch)
        {
            lastMouse = mouse;
            mouse += (int)Input.mouseScrollDelta.y;

            if (mouse <= 0)
            {
                mouse = 0;
            }
            else if (mouse >= gunlist.Count)
            {
                mouse = gunlist.Count - 1;
            }
        }

    }
}
