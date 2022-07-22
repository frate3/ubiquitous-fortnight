using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunController : MonoBehaviour
{
    bool canSwitch = true;
    [SerializeField] gun instance;
    [SerializeField] pistolScript instance1;
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject assaultRifle;
    [SerializeField] int mouse;
    List<GameObject> gunlist = new List<GameObject>();
    int startingWeapon = 1;
    GameObject currentWeapon;
    float timeForSwitch = 2;
    int lastMouse;
    // Start is called before the first frame update
    void Start()
    {
        addGuns();
        for (int i = 0; i < gunlist.Count; i++)
        {
            gunlist[i].SetActive(false);
            Debug.Log(gunlist);
        }
        mouse = startingWeapon;
    }

    // Update is called once per frame
    void Update()
    {

        inputGetter();
        weaponChange();
        Debug.Log(canSwitch);


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
                        canSwitch = false;
                        Invoke("reset", timeForSwitch);
                    }
                }
                
            }
        }
    }

    void switchWeapon()
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

                        currentWeapon = gunlist[i];
                    }
                    else if (mouse != lastMouse)
                    {

                        StartCoroutine(anim(gunlist[lastMouse], i));
                        Invoke("reset", timeForSwitch);
                        gunlist[i].SetActive(false);
                        canSwitch = false;

                    }
                }
            }

        }
    }

    private void reset()
    {
        canSwitch = true;
    }

    IEnumerator anim(GameObject gun, int num)
    {
        gun.GetComponent<Animator>().SetBool("switching", true);
        yield return new WaitForSeconds(1);


    }
    void addGuns()
    {
        gunlist.Add(pistol);
        gunlist.Add(assaultRifle);



    }

    void inputGetter()
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
