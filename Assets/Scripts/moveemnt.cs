using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveemnt : MonoBehaviour
{

    [Header("Positioning")]
    [Header("Changeble Values")]
    Vector3 direction;
    float lastSpeed;
    [SerializeField] GameObject touchGround;
    bool grounded;
    float speed = 8f;
    public float jumpHeight = 5f;
    public float camSpeed = 4f;
    CharacterController cc;
    float moveX;
    float moveZ;
    Vector3 moveDirection = Vector3.zero;
    [SerializeField] Camera cam;
    float camX;
    float camY;
    float sprintSpeed = 12f;
    public ProgressBar pb;
    public float health = 100;

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

        //if (!grounded)
        //{
        //    moveDirection.x = direction.x;
        //    moveDirection.z = direction.z;
            
        //}
    }

    void sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            
            lastSpeed = speed;
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
        }
    }

    void cameraMove()
    {
        transform.rotation = Quaternion.Euler(0, camX, 0);

        camY = Mathf.Clamp(camY, -90, 90);
        cam.transform.localRotation =  Quaternion.Euler(camY, 0, 0);
    }

    
    void ground()
    {
        grounded = Physics.Raycast(touchGround.transform.position, -transform.up, 0.1f, LayerMask.GetMask("ground"));
    }
    void Inputs()
    {
        {
            if (grounded)
            {
                moveX = Input.GetAxisRaw("Horizontal");
                moveZ = Input.GetAxisRaw("Vertical");
            }
            
            camX += Input.GetAxis("Mouse X") * camSpeed;
            camY -= Input.GetAxis("Mouse Y") * camSpeed;
        }
    }

    //clamp health at 100;
}
