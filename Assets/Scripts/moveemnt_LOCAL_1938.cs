using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveemnt : MonoBehaviour
{


    Vector3 direction;
    Vector3 moveDirection = Vector3.zero;

    [Header("Changeble Values")]
    [SerializeField] GameObject touchGround;
    [SerializeField] Camera cam;
    public ProgressBar pb;
    public ProgressBar spb;
    public float health = 100;
    public float sprintTime;
    public float jumpHeight = 5f;
    public float camSpeed = 4f;
    CharacterController cc;

    float lastSpeed;
    bool grounded;
    float speed = 8f;
    float moveX;
    float moveZ;
    float camX;
    float camY;
    float sprintSpeed = 12f;
    float slowSpeed = 4f;



    // Start is called before the first frame update
    void Start()
    {
        lastSpeed = speed;
        cc = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        ground();
        Inputs();
        cameraMove();
        gravity();
        sprint();
        pb.BarValue = health;
        spb.BarValue = sprintTime;


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

    void sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {

            speed = sprintSpeed;
        }
        else if (Input.GetButtonUp("Sprint"))
        {

            speed = lastSpeed;
        }

    }

    void gravity()
    {
        if (!grounded)
        {
            moveDirection.y += Physics.gravity.y * Time.deltaTime;
            speed = slowSpeed;
        }
        else
        {
            speed = lastSpeed;
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
        grounded = Physics.Raycast(touchGround.transform.position, -transform.up, 0.1f, LayerMask.GetMask("ground"));
    }
    void Inputs()
    {

        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");

        camX += Input.GetAxis("Mouse X") * camSpeed;
        camY -= Input.GetAxis("Mouse Y") * camSpeed;

    }

    //clamp health at 100;
}
