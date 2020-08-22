using UnityEngine;

public class LlamtaInvolcada : MonoBehaviour
{
    Transform target;
    GameObject targetObject;
    Rigidbody rig;
    public float speed = 10f;
    public float rotationSpeed = 6f;
    void moveLlamita()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        rig.MovePosition(pos);
        Vector3 targetDir = target.position - transform.position;
        targetDir.y = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), Time.time * rotationSpeed);
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
        targetObject = GameObject.FindWithTag("Player");
        target = targetObject.transform;
    }
    void Update()
    {
        moveLlamita();
    }
}
