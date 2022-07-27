using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveemnt : MonoBehaviour
{

    RaycastHit hit;
    Vector3 direction;
    Vector3 moveDirection = Vector3.zero;

    [Header("Changeble Values")]
    [SerializeField] GameObject touchGround;
    [SerializeField] Camera cam;
    public ProgressBar pb;
    public ProgressBar spb;
    public static float health = 100;
    public float jumpHeight = 5f;
    public float camSpeed = 4f;
    CharacterController cc;

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
    public static bool sprinting = false;
    float slowSpeed = 4f;
    bool inWater = false;
    bool flashState = true;
    public GameObject flashlight;
    float timeToCross = 6000;
    bool allowSprint;




    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = speed;
        lastSpeed = speed;
        cc = GetComponent<CharacterController>();
        if (flashlight) flashlight.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        print(Time.time + " Time");
        ground();
        Inputs();
        cameraMove();
        gravity();
        sprint();
        Terrain();
        Flash();
        GameEnd();
        if (pb != null)
        {
            pb.BarValue = health;
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

        cc.Move(moveDirection * Time.deltaTime);
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
        if (Input.GetButtonDown("Sprint"))
        {
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
    void GameEnd()
    {
        Collider[] contacts = Physics.OverlapSphere(transform.position, 30f);
        for (int i = 0; i < contacts.Length; i++)
        {
            if (contacts[i].tag == "end")
            {
                Death();
            }
        }

        if (maxSprintTime - Time.time <= 0)
        {
            print("gameEnd");
            Death();
        }
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

        camY = Mathf.Clamp(camY, -90, 90);
        cam.transform.localRotation = Quaternion.Euler(camY, 0, 0);
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
        camY -= Input.GetAxis("Mouse Y") * camSpeed;

    }


    void Terrain()
    {
        if (inWater)
        {
            allowSprint = false;
            speed = slowSpeed;
        } else
        {
            allowSprint = true;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        print(other);
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
    }


    void Death()
    {
        print("death");
        SceneManager.LoadScene("Game");
        //kill player and do other death things
    }


    public static void TakeDamage(float Damage)
    {
        health -= Damage;
    }
}
