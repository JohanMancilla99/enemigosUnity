using UnityEngine;

public class Llamita : MonoBehaviour
{
    bool detected;
    Transform target;
    Rigidbody rig;
    public float speed = 6f;
    public float rotationSpeed = 6f;
    float[] positionsX = new float[4];
    float[] positionsZ = new float[4];
    int pointIndex = 0;
    float pointRange = 10f;
    void moveDetected()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        rig.MovePosition(pos);
        Vector3 targetDir = target.position - transform.position;
        targetDir.y = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), Time.time * rotationSpeed);
    }
    void spawnPoints()
    {
        for(int x = 0; x < 4; x++)
        {
            positionsX[x] = Random.Range((transform.position.x - pointRange), (transform.position.x + pointRange));
            positionsZ[x] = Random.Range((transform.position.z - pointRange), (transform.position.z + pointRange));
        }
    }
    void moveToPoints()
    {
        Vector3 pointPosition = new Vector3(positionsX[pointIndex], (transform.position.y), positionsZ[pointIndex]);
        Vector3 pos = Vector3.MoveTowards(transform.position, pointPosition, speed * Time.fixedDeltaTime);
        rig.MovePosition(pos);
        Vector3 targetDir = pointPosition - transform.position;
        targetDir.y = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), Time.time * rotationSpeed);
        if (((transform.position.x > positionsX[pointIndex] - 1) && (transform.position.x < positionsX[pointIndex] + 1)) && ((transform.position.z > positionsZ[pointIndex] - 1) && (transform.position.z < positionsZ[pointIndex] + 1))) {
            pointIndex += 1;
        }
        if(pointIndex == 4)
        {
            pointIndex = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.transform;
            detected = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            detected = false;
            spawnPoints();
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
        spawnPoints();
    }
    void FixedUpdate()
    {
        if (detected)
        {
            moveDetected();
        } 
        else
        {
            moveToPoints();
        }
    }
}
