using DG.Tweening;
using UnityEngine;

public class ToStation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            bool parked = false;

            // Проверяем все парковки
            foreach (var parking in ParkingsStation.Instance.parkings)
            {
                // Если парковка свободна
                if (parking.transform.childCount == 1)
                {

                    other.transform.SetParent(parking.transform);
                    other.gameObject.transform.LookAt(new Vector3(transform.position.x, 0, parking.transform.position.z));
                    other.gameObject.transform.DOMove(new Vector3(transform.position.x, 0, parking.transform.position.z), 0.2f).OnComplete(() =>
                    {
                        other.gameObject.transform.DOMove(parking.transform.position, 0.2f).OnComplete(() =>
                        {
                            other.GetComponent<Car>().isParked = true;
                            other.GetComponent<Car>().SetFinalPos(parking.transform.position);
                        });
                    });


                    parked = true; // Устанавливаем флаг, что автомобиль припаркован

                    break; // Прерываем цикл после успешной парковки
                }
            }

            // Если не удалось припарковать, выводим сообщение
            if (!parked)
            {
                Debug.Log("Нет свободных парковок.");
            }
        }
    }
}
