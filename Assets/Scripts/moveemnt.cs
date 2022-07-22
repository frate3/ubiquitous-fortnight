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
    public float sprintTime = 500;
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
    public bool sprinting = false;
    float slowSpeed = 4f;
    float noMoveTime;
    bool inWater  = false;
    bool allowSprint;
    bool flashState = true;
    public GameObject flashlight;



    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = speed;
        maxSprintTime = sprintTime;
        lastSpeed = speed;
        cc = GetComponent<CharacterController>();
        flashlight.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update(){
        ground();
        Inputs();
        cameraMove();
        gravity();
        sprint();
        Terrain();
        Flash();
        if (pb != null)
        {
            pb.BarValue = health;
        }
        if (spb != null)
        {
            spb.BarValue = sprintTime / 5;
        }



        if (!sprinting){
            noMoveTime++;
        }

        if (health < 0){
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

    void Flash(){
        if (Input.GetButtonDown("FlashToggle")){
            if (flashState){
                flashlight.SetActive(false);
                flashState = false;
            } else {
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
        else 
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
            speed = slowSpeed;
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
        } else
        {
            inWater = false;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Lava")){
            Death();
        }
    }


    void Death(){
        print("death");
        //kill player and do other death things
    }


    public static void TakeDamage()
    {
        health--;
    }
}
