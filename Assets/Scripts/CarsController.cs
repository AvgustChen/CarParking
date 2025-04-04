using System.Collections.Generic;
using UnityEngine;

public class CarsController : MonoBehaviour
{
    public static CarsController Instance;
    public List<Car> carsList;
    public List<Car> carSelectedList;

    private void Start()
    {
        Instance = this;
        carSelectedList = new List<Car>();

        carsList = new List<Car>(FindObjectsOfType<Car>());
    }

    private void Update()
    {
        if (carsList.Count == 0)
        {
            // вызов события победы
            Debug.Log("You win");
        }
    }

    private void FixedUpdate()
    {
        if (carSelectedList != null)
        {
            List<Car> carsToRemove = new List<Car>();
            foreach (Car car in carSelectedList)
            {
                car.Move();
                if (car.isRemoved)
                    carsToRemove.Add(car);
            }

            foreach (Car car in carsToRemove)
            {
                carSelectedList.Remove(car);
                car.isRemoved = false;
                if (car.IsHasFinalPos())
                    carsList.Remove(car);
            }
        }
    }
}
