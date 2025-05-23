using System.Collections.Generic;
using UnityEngine;

public class CarsController : MonoBehaviour
{
    public static CarsController Instance;
    public List<Car> carsList;
    public List<Car> carSelectedList;

    private void Awake()
    {
        Instance = this;
        carSelectedList = new List<Car>();
    }

    private void Update()
    {
        if (carsList.Count == 0 && GameManager.Instance.GetIsGameStarted())
        {
            GameManager.Instance.WinGame();
        }
    }

    private void FixedUpdate()
    {
        if (carSelectedList.Count > 0)
        {
            // Удаляем автомобили, которые помечены как удаленные
            carSelectedList.RemoveAll(car => 
            {
                if (car.isRemoved)
                {
                    if (car.IsHasFinalPos())
                        carsList.Remove(car);
                    car.isRemoved = false;
                    return true; // Удаляем автомобиль из carSelectedList
                }
                car.Move(); // Двигаем автомобиль, если он не удален
                return false; // Не удаляем
            });
        }
    }

    public void FindCarsInLevel()
    {
        carsList = new List<Car>(FindObjectsByType<Car>(FindObjectsSortMode.None));
    }
}
