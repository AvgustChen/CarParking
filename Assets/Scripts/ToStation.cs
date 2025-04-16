using System.Linq;
using DG.Tweening;
using UnityEngine;

public class ToStation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Car car = other.GetComponent<Car>();

            // Проверяем, припаркован ли уже автомобиль
            if (car.isParked) return;

            Parking freeParking = ParkingsStation.Instance.parkings.FirstOrDefault(p => p.GetIsFree());

            if (freeParking != null)
            {
                car.AddCarToParking(freeParking);
                car.isParked = true; // Обновляем состояние автомобиля
                freeParking.SetIsFree(false); // Обновляем состояние парковки
                other.transform.SetParent(freeParking.transform);
                other.transform.LookAt(new Vector3(transform.position.x, 0, freeParking.transform.position.z));

                // Перемещение автомобиля к парковке
                other.transform.DOMove(new Vector3(transform.position.x, 0, freeParking.transform.position.z), 0.2f).OnComplete(() =>
                {
                    other.transform.DOMove(freeParking.transform.position, 0.2f).OnComplete(() =>
                    {
                        car.SetFinalPos(freeParking.transform.position);
                    });
                });
            }
            else
            {
                GameManager.Instance.LoseGame(); // Если нет свободных парковок
            }
        }
    }
}
