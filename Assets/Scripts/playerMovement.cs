using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    public int movementSpeed = 1;
    public int jumpStrength = 10;
    
    public bool inAir;
    public bool ethereal;

    public float etherealDuration = 1;

    private float _etherealDur;
    private Rigidbody rb;
    private Vector3 platformNormal;

    // Use this for initialization
    void Start() {
        if (GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update() {
        if (ethereal)
        {
            _etherealDur -= Time.deltaTime;
            if(_etherealDur <= 0)
            {
                Physics.IgnoreLayerCollision(29, 31, false);
                ethereal = false;
                rb.useGravity = true;
                _etherealDur = etherealDuration;
            }
        }
        physicsCheck();
        keyboardInput();
    }
    void keyboardInput()
    {
        if (!ethereal)
        {
            if (Input.GetKey(KeyCode.D))                                                    //right
            {
                Vector3 temp = Vector3.Cross(platformNormal, new Vector3(movementSpeed, 0, 0) );
                Vector3 dir =  Vector3.Cross(temp, platformNormal);

                rb.velocity += dir * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.A))                                               //left
            {
                Vector3 temp = Vector3.Cross(platformNormal, new Vector3(movementSpeed, 0, 0));
                Vector3 dir = Vector3.Cross(temp, platformNormal);

                rb.velocity -= dir * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.W) && !inAir)                                     //jump
            {
                float x = rb.velocity.x;
                rb.velocity = new Vector3(x, jumpStrength * Time.deltaTime, 0);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                ethereal = true;
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                Physics.IgnoreLayerCollision(29, 31);

            }
        }
        else
        {
            if (Input.GetKey(KeyCode.D))                                                    //right
            {
                transform.position += new Vector3(movementSpeed, 0, 0) * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.A))                                               //left
            {
                transform.position += new Vector3(-movementSpeed, 0, 0) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.W))                                        //float up
            {
                transform.position += new Vector3(0, movementSpeed, 0) * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))                                     //float down
            {
                transform.position += new Vector3(0 , -movementSpeed, 0) * Time.deltaTime;
            }
        }
    }

    void physicsCheck()
    {
        RaycastHit ray;
        if(Physics.Raycast(transform.position, Vector3.down , out ray , 0.2f))
        {
            inAir = false;
            platformNormal =  ray.normal;
        }
        else
        {
            inAir = true;
        }
    }


    


}


