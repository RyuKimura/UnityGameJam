using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    public int movementSpeed = 1;
    public int jumpStrength = 10;

    public bool inAir;
    public bool canMoveRight =true;
    public bool canMoveLeft  =true;
    private Rigidbody rb;

    // Use this for initialization
    void Start() {
        if (GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update() {
        physicsCheck();
        debugs();
        keyboardInput();
    }



    void keyboardInput()
    {
        if (Input.GetKey(KeyCode.D))                                                    //right
        {
            rb.velocity += new Vector3(movementSpeed, 0, 0) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A) )                                               //left
        {
            rb.velocity += new Vector3(-movementSpeed, 0, 0) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.W) && !inAir)                                     //jump
        {
            rb.velocity = new Vector3(0, jumpStrength, 0);
        }
    }

    

    void physicsCheck()
    {
        if(Physics.Raycast(transform.position, Vector3.down,  1))
        {
            inAir = false;
        }
        else
        {
            inAir = true;
        }

        //if (Physics.Raycast(transform.position, Vector3.right, movementSpeed * Time.deltaTime))
        //{
        //    canMoveRight = false;
        //}
        //else canMoveRight = true;

        //if (Physics.Raycast(transform.position, Vector3.left, -movementSpeed * Time.deltaTime))
        //{
        //    canMoveLeft = false;
        //}
        //else canMoveLeft = true;
    }

    void debugs()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.right , Color.red);
        Debug.DrawLine(transform.position, transform.position + Vector3.left, Color.red);
    }

}


