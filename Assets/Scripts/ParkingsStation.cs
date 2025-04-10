using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class ParkingsStation : MonoBehaviour
{
    public event EventHandler OnHumanSeat;
    public static ParkingsStation Instance;
    public Transform[] parkings;
    public Vector3 finalPos;
    private bool isCanSeat;
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
            foreach (Transform parking in parkings)
            {
                if (parking.childCount > 1 && HumansManager.Instance.humansList.Count > 0)
                {
                    Car car = parking.GetChild(1).GetComponent<Car>();
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
        human.gameObject.transform.LookAt(car.transform.position);
        human.transform.SetParent(car.transform);
        car.countPass++;

        // Удаляем человека из списка
        HumansManager.Instance.humansList.RemoveAt(0);
        // Устанавливаем таймер для следующей посадки
        timerToSeat = 0.2f;
        HumansManager.Instance.OneStepSteckHumans();


        // Анимация перемещения человека в автомобиль
        human.gameObject.transform.DOMove(car.transform.position, 0.8f).OnComplete(() =>
        {
            human.gameObject.SetActive(false);
            OnHumanSeat?.Invoke(this, EventArgs.Empty);

            Vector3 v = car.transform.localScale;
            car.transform.DOScale(v + new Vector3(0.2f, 0.2f, 0.2f), 0.1f).OnComplete(() =>
            {
                car.transform.DOScale(v, 0.1f);
                // Проверяем, заполнен ли автомобиль
                if (car.countPass == car.numberOfSeats)
                {
                    car.transform.SetParent(null);
                    car.finalPosition = finalPos;
                    OnHumanSeat?.Invoke(this, EventArgs.Empty);
                }
            });
        });
        isCanSeat = true;
    }
}
