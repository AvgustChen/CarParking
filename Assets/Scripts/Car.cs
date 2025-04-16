using UnityEngine;


public class Car : MonoBehaviour
{
    [SerializeField] private string color;
    private Rigidbody rb;
    [SerializeField] private float speed = 5f, finalSpeed = 15f, rotationSpeed = 50f;

    public Vector3 finalPosition, startPosition, startScale;
    public int numberOfSeats, countPass;

    public bool isReturn, isRemoved, isActive, isParked;
    private CarUI carUI;
    private Parking parking;

    private void Awake()
    {
        carUI = GetComponent<CarUI>();
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        startScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.GetIsGameStarted())
        {
            CarsController.Instance.carSelectedList.Add(this);
            isActive = true;
            carUI.StartMoveSound();
        }
    }

    public void Move()
    {
        if (isReturn)
        {
            ReturnStartPosition();
        }

        if (finalPosition == Vector3.zero)
        {
            rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);
        }

        if (finalPosition != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPosition, finalSpeed * Time.fixedDeltaTime);

            Vector3 lookAtPos = finalPosition - transform.position;
           transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookAtPos), rotationSpeed * Time.deltaTime);
        }
    }

    private void ReturnStartPosition()
    {
        if (isReturn)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, finalSpeed * Time.deltaTime);
            if (transform.position == startPosition)
            {
                isActive = false;
                isRemoved = true;
                isReturn = false;
            }
        }
    }

    private void CarDestroy()
    {
        Destroy(gameObject);
    }

    public void RemoveCarFromParking()
    {
        parking.SetIsFree(true);
        parking = null;
    }

    public void AddCarToParking(Parking parking)
    {
        this.parking = parking;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if (!other.GetComponent<Car>().isActive)
            {
                isReturn = true;
                carUI.CrashSound();
            }
        }
        else if (other.CompareTag("Finish"))
        {
            isRemoved = true;
            Invoke("CarDestroy", 0.2f);
        }

    }

    public void SetFinalPos(Vector3 vector3)
    {
        finalPosition = vector3;
    }

    public bool IsHasFinalPos()
    {
        if (finalPosition == Vector3.zero)
            return false;
        return true;
    }

    public string GetColor()
    {
        return color;
    }

    public Parking GetParking()
    {
        return parking;
    }
}
