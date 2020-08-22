using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Arbol : MonoBehaviour
{
    bool detected;
    bool follow;
    Transform target;
    Rigidbody rig;
    public float speed = 6f;
    public float rotationSpeed = 2f;
    public GameObject llamita;
    public float timeToSpawn = 3;
    float timeSaver;
    public Transform spawnDirection;
    void moveDetected()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        rig.MovePosition(pos);
        Vector3 targetDir = target.position - transform.position;
        targetDir.y = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), Time.time * rotationSpeed);
    }
    void spawnEnemy()
    {
        if(timeToSpawn < 0)
        {
            Instantiate(llamita, spawnDirection.position, spawnDirection.rotation);
            timeToSpawn = timeSaver;
        }
        timeToSpawn = timeToSpawn - Time.deltaTime;
    }

    void lookInZone()
    {
        Vector3 targetDir = target.position - transform.position;
        targetDir.y = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), Time.time * rotationSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.transform;
            detected = true;
            follow = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            follow = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        timeSaver = timeToSpawn;
    }
    void FixedUpdate()
    {
        if (detected)
        {
            if(follow)
            {
                moveDetected();
            } else
            {
                lookInZone();
            }
           spawnEnemy();
        }
    }
}
