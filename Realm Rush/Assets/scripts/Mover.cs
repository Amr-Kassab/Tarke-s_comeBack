using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Rigidbody rb;
    public Transform t;
    public Transform tr;
    private Animator anim;
    [SerializeField] float runningspeed = 100f;
    [SerializeField] float thrust = 100f;
    [SerializeField] float rotation ;
    [SerializeField] float movingspeed = 80f;
    [SerializeField] float rotationtime = 0.1f;
    float gravity;
    //  [SerializeField] float GroundCheckDistance = 1f;
    //  [SerializeField] LayerMask GroundMask;
    // [SerializeField] bool IsGrounded;
     CharacterController controll;

    // void OnCollisionEnter(Collision other) 
    // {
    //     if (other.gameObject.tag=="Ground")
    //     {
    //         gravity = 0f;
    //         // IsGrounded = true;
    //     }
    // }
    //  void OnCollisionExit(Collision other)
    // {
    //     if (other.gameObject.tag=="Ground")
    //     {
    //         gravity = -9.81f;
    //         // IsGrounded = false;
    //     }
    // }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        t = GetComponent<Transform>();
        tr = GetComponent<Transform>();
        controll = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        // checkingsphere();
        processflying();
        processmoving();
        // processRotation();
    }
    void processflying()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrust);
        }
    }
     void processmoving()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal , 0f , vertical).normalized;


        if(direction.magnitude >= 0.1f)
        {
            float targetangle = Mathf.Atan2(direction.x , direction.z) * Mathf.Rad2Deg + t.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y , targetangle , ref rotation , rotationtime );
            Vector3 moveDir = Quaternion.Euler(0f , targetangle , 0f) * Vector3.forward;
            Vector3 movement = moveDir.normalized * movingspeed * Time.deltaTime;
            if(Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetFloat("speed", 1f ,0.1f , Time.deltaTime);
                transform.rotation = Quaternion.Euler( 0f , angle , 0f);
                // controll.Move(moveDir.normalized * runningspeed * Time.deltaTime);
                rb.velocity = movement;
            }
            anim.SetFloat("speed", 0.5f,0.1f , Time.deltaTime);
            transform.rotation = Quaternion.Euler( 0f , angle , 0f);
            // controll.Move(moveDir.normalized * movingspeed * Time.deltaTime);
            rb.velocity = movement;
        }
        else {anim.SetFloat("speed" , 0,0.1f , Time.deltaTime);}
        // if(IsGrounded)
        // {
            // runningspeed = 100f;
            // movingspeed = 80f;
            // if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            // {
            //     //rb.velocity = new Vector3 (-movingspeed , 0 , 0);
            //     anim.SetFloat("speed", 1f ,0.1f , Time.deltaTime);
            //      rb.AddRelativeForce(0 , 0 , runningspeed);
            //     // controll.Move(new Vector3( 0 , 0 , runningspeed));
            // }
            // else if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
            // {
            //     transform.Rotate(0 , 90f , 0);
            //     anim.SetFloat("speed", 1f,0.1f , Time.deltaTime);
            //     //rb.velocity = new Vector3 (-movingspeed , 0 , 0);
            //      rb.AddRelativeForce(0 , 0 , runningspeed);
            //     // controll.Move(new Vector3( 0 , 0 , -runningspeed));
            // }
            // else if(Input.GetKey(KeyCode.W))
            // {
            //     //rb.velocity = new Vector3 (-movingspeed , 0 , 0);
            //     anim.SetFloat("speed", 0.5f,0.1f , Time.deltaTime);
            //      rb.AddRelativeForce(0 , 0 , movingspeed);
            //     // controll.Move(new Vector3( 0 , 0 , movingspeed));
            // }
            // else if(Input.GetKey(KeyCode.S))
            // {
            //     transform.Rotate(0 , 90f , 0);
            //     transform.rotation = Quaternion.Euler( 0 , 90 , 0);
            //     anim.SetFloat("speed", 0.5f,0.1f , Time.deltaTime);
            //     //rb.velocity = new Vector3 (movingspeed , 0 , 0);
            //      rb.AddRelativeForce(0 , 0 , movingspeed);
            //     // controll.Move(new Vector3( 0 , 0 , runningspeed));
            // }
            // else
            // {
            //     movingspeed = 0;
            //     runningspeed = 0;
            //     anim.SetFloat("speed" , 0,0.1f , Time.deltaTime);
            // }
        // }
        // else if(!IsGrounded)
        // {
        //     movingspeed = 0;
        //     runningspeed = 0;
        //     anim.SetFloat("speed" , 0,0.1f , Time.deltaTime);
        // }
    }
    // void checkingsphere()
    // {
    //     // IsGrounded=Physics.CheckSphere(transform.position , GroundCheckDistance , GroundMask);
    // }
    // void processRotation ()
    // {
    //     if (Input.GetKey(KeyCode.A))
    //     {
    //         ApplyRotation(-rotation);
    //     }
    //     else if (Input.GetKey(KeyCode.D))
    //     {
    //         ApplyRotation(rotation);
    //     }
    // }

    // private void ApplyRotation(float rotationthisframe)
    // {
    //     rb.freezeRotation = false;
    //     gameObject.transform.Rotate(Vector3.up * rotationthisframe * Time.deltaTime);
    //     rb.freezeRotation = true;
    // }
}
