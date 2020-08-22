using UnityEngine;

public class LlamitaSpawner : MonoBehaviour
{
    public GameObject llamita;
    public Transform spawn;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Instantiate(llamita, spawn.position, spawn.rotation);
        }  
    }
}
