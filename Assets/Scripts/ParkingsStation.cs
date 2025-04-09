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
    bool isCanSeat;
    float timerToSeat;

    private void Awake()
    {
        isCanSeat = true;
        Instance = this;
    }
    public void Update()
    {
        if (isCanSeat)
        {
            foreach (Transform parking in parkings)
            {
                if (parking.childCount > 1)
                {
                    Car car = parking.GetChild(1).GetComponent<Car>();
                    Human human = HumansManager.Instance.humansList[0].GetComponent<Human>();
                    if (car.GetColor() == human.GetColor() && car.isParked && car.countPass < car.numberOfSeats)
                    {
                        SeatHumanInCar(human, car);
                        break;
                    }
                }
            }
        }
    }

    // public void TrySeatHumanInCar(Transform parking)
    // {
    //     Car car = parking.GetChild(1).GetComponent<Car>();
    //     if (car == null) return;

    //     if (HumansManager.Instance.humansList.Count > 0)
    //     {
    //         Human human = HumansManager.Instance.humansList[0].GetComponent<Human>();
    //         if (car.GetColor() == human.GetColor())
    //         {
    //             //isCanSeat = false;
    //             SeatHumanInCar(human, car);
    //         }

    //     }
    // }

    private void SeatHumanInCar(Human human, Car car)
    {
        isCanSeat = false;
        human.gameObject.transform.LookAt(car.transform.position);
        human.transform.SetParent(car.transform);
        timerToSeat = 0.5f;
        car.countPass++;
        human.gameObject.transform.DOMove(car.transform.position, 0.5f).OnComplete(() =>
        {
            human.gameObject.SetActive(false);
            OnHumanSeat?.Invoke(this, EventArgs.Empty);
            Vector3 v = car.transform.localScale;
            car.transform.DOScale(v + new Vector3(0.2f, 0.2f, 0.2f), 0.1f).OnComplete(() =>
            {
                car.transform.DOScale(v, 0.1f);
            });
            if (car.countPass == car.numberOfSeats)
            {
                car.transform.SetParent(null);
                car.finalPosition = finalPos;
                OnHumanSeat?.Invoke(this, EventArgs.Empty);
            }

            HumansManager.Instance.OneStepSteckHumans();
            HumansManager.Instance.humansList.RemoveAt(0);
            StartGame.Instance.CountUpdate();
            isCanSeat = true;
        });


    }
}
