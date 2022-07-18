using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveemnt : MonoBehaviour
{

    [SerializeField] GameObject touchGround;
    bool grounded;
    public float speed = 8f;
    public float jumpHeight = 5f;
    public float camSpeed = 4f;
    CharacterController cc;
    float moveX;
    float moveZ;
    Vector3 moveDirection = Vector3.zero;
    [SerializeField] Camera cam;
    float camX;
    float camY;
    // Start is called before the first frame update
    void Start()
    {
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
        jump();
        

        moveDirection.x = moveX * speed;
        moveDirection.z = moveZ * speed;

        moveDirection = transform.TransformDirection(moveDirection);

        

        cc.Move(moveDirection * Time.deltaTime);
    }

    void jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            moveDirection.y = jumpHeight;
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
        transform.rotation = Quaternion.Euler(0, camX * camSpeed, 0);
        cam.transform.localRotation = Quaternion.Euler(camY * camSpeed, 0, 0);
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
            
            camX += Input.GetAxis("Mouse X");
            camY -= Input.GetAxis("Mouse Y");
        }
    }
}
