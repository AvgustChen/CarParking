using UnityEngine;

public class ToStation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            foreach (Transform parking in ParkingsStation.Instance.parkings)
            {
                if (parking.transform.childCount == 1)
                {
                    other.GetComponent<Car>().SetFinalPos(parking.position);
                    other.transform.SetParent(parking);
                }
            }
        }
    }
}
