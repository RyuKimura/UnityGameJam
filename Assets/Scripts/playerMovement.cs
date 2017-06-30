using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour {

    public int movementSpeed = 1;
    public int jumpStrength = 10;
    
    public bool inAir;
    public bool ethereal;

    public float etherealDuration = 1;
    public float etherealCooldown = 3;

    public GameObject cooldownTimer;

    private float _etherealDur;
    private float _etherealCD;
    private Rigidbody rb;
    private Vector3 platformNormal;

    // Use this for initialization
    void Start() {
        if (GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
        }
        _etherealCD = 3;
        cooldownTimer.GetComponent<Image>().fillAmount = _etherealCD / etherealCooldown;
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(platformNormal);
        Debug.DrawRay(transform.localPosition, new Vector3(0, -0.2f, 0), Color.red);
        if (ethereal)
        {
            _etherealDur -= Time.deltaTime;
            if(_etherealDur <= 0)
            {
                Physics.IgnoreLayerCollision(29, 30, false);
                ethereal = false;
                rb.useGravity = true;
                _etherealDur = etherealDuration;
            }
        }

        if (_etherealCD < etherealCooldown)
        {
            _etherealCD += Time.deltaTime;
            cooldownTimer.GetComponent<Image>().fillAmount = _etherealCD / etherealCooldown ;
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
                Vector3 temp = Vector3.Cross(platformNormal, new Vector3(movementSpeed, 0, 0));
                Vector3 dir = Vector3.Cross(temp, platformNormal);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                rb.velocity += dir * Time.deltaTime;

            }
            else if (Input.GetKey(KeyCode.A))                                               //left
            {
                Vector3 temp = Vector3.Cross(platformNormal, new Vector3(movementSpeed, 0, 0));
                Vector3 dir = Vector3.Cross(temp, platformNormal);
                transform.rotation = Quaternion.Euler(0, 270, 0);
                rb.velocity -= dir * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.W) && !inAir)                                     //jump
            {
                float x = rb.velocity.x;
                rb.velocity = new Vector3(x, jumpStrength * Time.deltaTime, 0);
            }

            if (Input.GetKey(KeyCode.Space) && _etherealCD >= etherealCooldown)
            {
                _etherealCD = 0;
                ethereal = true;
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                Physics.IgnoreLayerCollision(29, 30);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.D))                                                    //right
            {
                transform.position += new Vector3(movementSpeed, 0, 0) * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (Input.GetKey(KeyCode.A))                                               //left
            {
                transform.position += new Vector3(-movementSpeed, 0, 0) * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 270, 0);
            }
            if (Input.GetKey(KeyCode.W))                                        //float up
            {
                transform.position += new Vector3(0, movementSpeed, 0) * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))                                     //float down
            {
                transform.position += new Vector3(0, -movementSpeed, 0) * Time.deltaTime;
            }
        }
    }

    void physicsCheck()
    {
        RaycastHit ray;
        if(Physics.Raycast(transform.localPosition, Vector3.down , out ray , 0.2f))
        {
            Debug.Log(ray.transform.name);
            inAir = false;
            platformNormal =  ray.normal;
        }
        else
        {
            inAir = true;
            platformNormal = Vector3.up;
        }
    }


    


}


