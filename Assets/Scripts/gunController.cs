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
        for (int i = 0; i < gunlist.Count; i++) gunlist[i].SetActive(false);
        mouse = startingWeapon;
    }

    // Update is called once per frame
    void Update()
    {

        inputGetter();
        switchWeapon();
        
        
    }

    void switchWeapon ()
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
                    //Debug.Log("can change");
                    StartCoroutine(anim(gunlist[lastMouse], i));
                }
            }
        }
    }

    

    IEnumerator anim(GameObject gun, int num)
    {
        gun.GetComponent<Animator>().SetBool("switching", true);
        yield return new WaitForSeconds(1);
        gunlist[num].SetActive(false);

    }
    void addGuns ()
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
        Debug.Log(mouse);
        

    }
}
