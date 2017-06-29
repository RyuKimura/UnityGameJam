using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    public int movementSpeed = 1;
    public int jumpStrength = 10;

    private bool inAir;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        if (GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        physicsCheck();
        keyboardInput();
    }



    void keyboardInput()
    {
        if (Input.GetKey(KeyCode.D))                                                    //right
        {
            transform.position += new Vector3(movementSpeed, 0, 0) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))                                               //left
        {
            transform.position += new Vector3(-movementSpeed, 0, 0) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.W) && !inAir)                                     //jump
        {
            rb.AddForce(new Vector3(0, jumpStrength, 0), ForceMode.Impulse);
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
    }

}


