using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    //public enum direction
    //{
    //    left = -1, right = 1
    //}

    //public direction initialDirection;
    public int initialDirection = 1;
    public int moveSpeed;
    public GameObject[] boundadries;

    private Rigidbody rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        rb.velocity = new Vector3(moveSpeed * initialDirection, 0, 0) * Time.deltaTime;
	}

    void OnCollisionEnter(Collision collision)
    {
        initialDirection *= -1;
    }
}
