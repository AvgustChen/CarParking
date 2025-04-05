using UnityEngine;


public class Car : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 5f, finalSpeed = 15f, rotationSpeed = 50f;

    private Vector3 finalPosition, startPosition;

    public bool isReturn, isRemoved;

    private void Awake()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        if (StartGame.isGameStarted)
        {
            CarsController.Instance.carSelectedList.Add(this);
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
            transform.position = Vector3.MoveTowards(transform.position, finalPosition, finalSpeed * Time.deltaTime);

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
                isRemoved = true;
                isReturn = false;
            }
        }
    }

    private void CarDestroy()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car") || other.CompareTag("Barrier"))
        {
            isReturn = true;
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
}
