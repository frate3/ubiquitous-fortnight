using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class moveemnt : MonoBehaviour
{

    RaycastHit hit;
    Vector3 direction;
    Vector3 moveDirection = Vector3.zero;

    public float Health = 100;
    public Damage damage = new Damage();

    [Header("Changeble Values")]
    [SerializeField] GameObject touchGround;
    [SerializeField] Camera cam;
    public ProgressBar pb;
    public ProgressBar spb;
    public float sprintTime = 500;
    public float jumpHeight = 5f;
    public float camSpeed = 4f;
    CharacterController cc;
    [SerializeField] Text tx;
    [SerializeField] Animator anim;

    bool isTouching;
    float lastSpeed;
    bool grounded;
    float speed = 8f;
    float baseSpeed;
    float moveX;
    float moveZ;
    float camX;
    float camY;
    float sprintSpeed = 12f;
    float maxSprintTime;
    public bool sprinting = false;
    float slowSpeed = 4f;
    float noMoveTime;
    bool inWater = false;
    bool allowSprint;
    bool flashState = true;
    public GameObject flashlight;
    [SerializeField] float boostSpeed = 1;



    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = speed;
        maxSprintTime = sprintTime;
        lastSpeed = speed;
        cc = GetComponent<CharacterController>();
        if (flashlight) flashlight.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public float health
    {
        get => Health;
        set
        {
            Health = Mathf.Clamp(value, 0, 100);
        }
    }

    public float CamY
    {
        get => camY;
        set
        {
            camY = Mathf.Clamp(value, -89, 90);
        }


    }

    void Update()
    {
        ground();
        Inputs();
        cameraMove();
        gravity();
        sprint();
        Terrain();
        Flash();
        onTriggerMe();
        //Objective();
        if (pb != null)
        {
            pb.BarValue = health;
        }
        if (spb != null)
        {
            spb.BarValue = sprintTime / 5;
        }



        if (!sprinting)
        {
            noMoveTime++;
        }

        if (health < 0)
        {
            Death();
            // SceneManager.LoadScene("TestScene");
            //do death things
        }

        moveDirection.x = moveX * speed;
        moveDirection.z = moveZ * speed;

        moveDirection = transform.TransformDirection(moveDirection);

        jump();

        Debug.Log(moveDirection.magnitude);
        anim.speed = (moveDirection.magnitude - 5) * boostSpeed / 5;



        cc.Move(moveDirection * Time.deltaTime);
    }

    void Objective()
    {
        tx.text = "Objective: Survive \n" + Mathf.Round(Time.time).ToString();
    }


    void jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            moveDirection.y = jumpHeight;
            direction = moveDirection;
        }


    }

    void Flash()
    {
        if (Input.GetButtonDown("FlashToggle"))
        {
            if (flashState)
            {
                flashlight.SetActive(false);
                flashState = false;
            }
            else
            {
                flashlight.SetActive(true);
                flashState = true;
            }

        }
    }

    void sprint()
    {
        if (sprinting && sprintTime >= 0)
        {
            sprintTime--;
        }
        else if (sprintTime != maxSprintTime)
        {


            sprinting = false;
        }

        if (sprintTime <= 0 || noMoveTime > 300)
        {

            noMoveTime = 0;
            Invoke("resetSprintTime", 3);
        }

        if (Input.GetButtonDown("Sprint") && sprintTime >= 0 && allowSprint)
        {
            noMoveTime = 0;
            sprinting = true;
            lastSpeed = speed;
            speed = sprintSpeed;

        }
        else if (!grounded)
        {

            sprinting = false;
            speed = baseSpeed;


        }

    }

    void resetSprintTime()
    {
        sprintTime = maxSprintTime;

        speed = lastSpeed;
    }



    void gravity()
    {
        if (!grounded)
        {
            moveDirection.y += Physics.gravity.y * Time.deltaTime;
        }

    }

    void cameraMove()
    {
        transform.rotation = Quaternion.Euler(0, camX, 0);

        cam.transform.localRotation = Quaternion.Euler(CamY, 0, 0);
    }


    void ground()
    {
        if (Physics.OverlapSphere(touchGround.transform.position, 0.5f, LayerMask.GetMask("ground")).Length != 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
    void Inputs()
    {

        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");

        camX += Input.GetAxis("Mouse X") * camSpeed;
        CamY -= Input.GetAxis("Mouse Y") * camSpeed;

    }


    void Terrain()
    {
        if (inWater)
        {
            allowSprint = false;
            speed = slowSpeed;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            print("in Water");
            flashlight.SetActive(false);
            flashState = false;
            inWater = true;
        }
        else
        {
            inWater = false;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Lava"))
        {
            Death();
        }

        if (other.CompareTag("health"))
        {
            health = Damage.AddHealth(health, 20);
            Destroy(other.gameObject);
        }
    }

    void onTriggerMe()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, 0.8f, LayerMask.GetMask("Enemy"));
        if (col.Length > 0)
        {
            
            /*for (int i = 0; i < col.Length; i++) Debug.Log(col[i].gameObject.name);*/
            health = Damage.TakeDamage(health, 8 * Time.deltaTime);
        }
    }


    void Death()
    {
        print("death");
        //kill player and do other death things
    }



}
