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
    // Start is called before the first frame update
    void Start()
    {
        addGuns();
        mouse = startingWeapon;

    }

    // Update is called once per frame
    void Update()
    {

        inputGetter();
        switchWeapon();


    }

    void switchWeapon()

    {

        if (!instance.reload && !instance1.reload)
        {
            for (int i = 0; i < gunlist.Count; i++)
            {
                if (i == mouse)
                {
                    Debug.Log("true");
                    gunlist[i].SetActive(true);
                    currentWeapon = gunlist[i];
                    
                }
                else
                {
                    Debug.Log("false");
                    if (gunlist[i].activeSelf)
                    {
                        Debug.Log("i need to know");
                        gunlist[i].GetComponent<Animator>().SetBool("switching", true);
                        gunlist[i].SetActive(false);
                    }

                }
            }
        }
    }





    void addGuns()
    {
        gunlist.Add(pistol);
        gunlist.Add(assaultRifle);



    }

    void inputGetter()
    {
        mouse += (int)Input.mouseScrollDelta.y;
        if (mouse < 0)
        {
            mouse = 0;
        }
        else if (mouse > gunlist.Count)
        {
            mouse = gunlist.Count;
        }
    }
}
