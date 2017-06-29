using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public int initialDirection = 1;
    public int moveSpeed;
    public GameObject[] boundadries;

    private Rigidbody rb;
    private Vector3 platformNormal;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        RaycastHit ray;
        Physics.Raycast(transform.position, Vector3.down, out ray, 0.2f);
        platformNormal = ray.normal;


        Vector3 temp = Vector3.Cross(platformNormal, new Vector3(moveSpeed, 0, 0));
        Vector3 dir = Vector3.Cross(temp, platformNormal);
        Debug.Log(platformNormal);
        rb.velocity += dir * initialDirection * Time.deltaTime;
        //rb.velocity += new Vector3(moveSpeed * initialDirection, 0, 0) * Time.deltaTime;
	}

    void OnCollisionEnter(Collision collision)
    {
        initialDirection *= -1;
    }
}
