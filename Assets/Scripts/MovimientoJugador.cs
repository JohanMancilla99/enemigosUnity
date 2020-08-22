using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    Rigidbody rig;

    float moveForward;
    float moveSide;
    float moveUp;
    bool inFloor;
    public float speed = 5f;
    public float jumpSpeed = 5f;
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveForward = Input.GetAxis("Vertical") * speed;
        moveSide = Input.GetAxis("Horizontal") * speed;
        moveUp = Input.GetAxis("Jump") * jumpSpeed;

        rig.velocity = (transform.forward * moveForward) + (transform.right * moveSide) + (transform.up * rig.velocity.y);

        if (inFloor)
        {
            rig.AddForce(transform.up * moveUp, ForceMode.VelocityChange);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            inFloor = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            inFloor = false;
        }
    }
}

