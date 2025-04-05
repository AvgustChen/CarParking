
using UnityEngine;

public class RoadFinishTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 finalPosition;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Car"))
        {
            other.GetComponent<BoxCollider>().enabled = false;
            other.GetComponent<Car>().SetFinalPos(finalPosition);
            other.GetComponent<Car>().isActive = true;
        }
    }
}
