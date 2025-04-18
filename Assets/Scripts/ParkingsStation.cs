using System;
using DG.Tweening;
using UnityEngine;

public class ParkingsStation : MonoBehaviour
{
    public static ParkingsStation Instance;
    public Parking[] parkings;
    public Vector3 finalPos;
    public bool isCanSeat;
    private float timerToSeat;

    private void Awake()
    {
        isCanSeat = true;
        Instance = this;
    }

    private void Update()
    {
        timerToSeat -= Time.deltaTime;

        // Проверяем, можно ли садиться в автомобиль, и таймер истек
        if (isCanSeat && timerToSeat <= 0)
        {
            foreach (Parking parking in parkings)
            {
                if (!parking.GetIsFree() && HumansManager.Instance.humansList.Count > 0)
                {
                    Car car = parking.transform.GetChild(1).GetComponent<Car>();
                    Human human = HumansManager.Instance.humansList[0].GetComponent<Human>();
                    // Проверяем, подходит ли человек для посадки
                    if (car.GetColor() == human.GetColor() && car.isParked && car.countPass < car.numberOfSeats)
                    {
                        SeatHumanInCar(human, car);
                        break; // Выходим из цикла после первой успешной посадки
                    }
                }
            }
        }
    }

    private void SeatHumanInCar(Human human, Car car)
    {
        isCanSeat = false; // Запретить посадку до завершения текущей
        // Удаляем человека из списка
        HumansManager.Instance.humansList.RemoveAt(0);
        HumansManager.Instance.OneStepSteckHumans();
        car.countPass++;
        // Устанавливаем таймер для следующей посадки
        timerToSeat = 0.2f;
        // Анимация перемещения человека в автомобиль
        human.SetFinalPos(car);

        isCanSeat = true;
    }
}
