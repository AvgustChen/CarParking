using System;
using DG.Tweening;
using UnityEngine;


public class Human : MonoBehaviour
{
    [SerializeField] private string color;
    private Vector3 finalPos;
    float finalSpeed = 15f;
    float rotationSpeed = 3500f;
    private Car car;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        if (finalPos != Vector3.zero)
            Move();
        if (car != null)
        {
            if (Vector3.Distance(transform.position, car.transform.position) < 1 && finalPos != Vector3.zero)
            {
                SeatInCar();
            }
        }
    }

    private void Move()
    {
        anim.SetBool("isWalk", true);
        Vector3 lookAtPos = finalPos - transform.position;
        if (transform.position.z == car.transform.position.z)
        {
            lookAtPos = finalPos - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, finalPos, finalSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0, this.car.transform.position.z), finalSpeed * Time.deltaTime);
            lookAtPos = new Vector3(transform.position.x, 0, this.car.transform.position.z) - transform.position;
        }
        if (lookAtPos != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookAtPos), rotationSpeed * Time.deltaTime);
    }
    public string GetColor()
    {
        return color;
    }

    public void SetFinalPos(Car car)
    {
        this.car = car;
        finalPos = this.car.transform.position;
    }

    public void SeatInCar()
    {
        transform.SetParent(car.transform.GetChild(0));
        car.GetComponent<CarUI>().HumanSeat();
        car.transform.DOScale(car.startScale + new Vector3(0.2f, 0.2f, 0.2f), 0.2f).OnComplete(() =>
        {
            car.transform.DOScale(car.startScale, 0.2f).OnComplete(() =>
            {
                GameManager.Instance.HumanSeat();
                if (car.transform.GetChild(0).childCount == car.numberOfSeats)
                {
                    if(car.GetParking() != null)
                        car.RemoveCarFromParking();
                    car.transform.SetParent(null);
                    car.finalPosition = ParkingsStation.Instance.finalPos;
                }
            });
        });
        gameObject.SetActive(false);
    }


}
