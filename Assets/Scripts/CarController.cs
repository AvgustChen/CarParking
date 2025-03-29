using System;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 5f, finalSpeed = 15f, rotationSpeed = 50f;
    private bool isClicked;
    private Vector3 finalPosition;
    private Vector3 startPosition;
    private bool isReturnStart;

    private float curPointX, curPointY;

    private enum Axis
    {
        vertical, horizontal
    }

    [SerializeField] private Axis carAxis;

    private enum Direction
    {
        Right, Left, Top, Bottom, None
    }

    private Direction carDirectionX = Direction.None, carDirectionY = Direction.None;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        startPosition = transform.position;       
    }

    private void OnMouseDown()
    {
        curPointX = Input.mousePosition.x;
        curPointY = Input.mousePosition.y;
    }

    private void OnMouseUp()
    {
        isClicked = true;
        if (Input.mousePosition.x - curPointX > 0 && carAxis == Axis.horizontal)
            carDirectionX = Direction.Right;
        else if(Input.mousePosition.x - curPointX < 0 && carAxis == Axis.horizontal)
            carDirectionX = Direction.Left;

        if (Input.mousePosition.y - curPointY > 0 && carAxis == Axis.vertical)
            carDirectionY = Direction.Top;
        else if (Input.mousePosition.y - curPointY < 0 && carAxis == Axis.vertical)
            carDirectionY = Direction.Bottom;
    }

    private void Update()
    {
        if (finalPosition != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPosition, finalSpeed * Time.deltaTime);

            Vector3 lookAtPos = finalPosition - transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookAtPos), rotationSpeed * Time.deltaTime);
        }
        else if(isReturnStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, finalSpeed * Time.deltaTime);
        }

        if (transform.position == finalPosition)
            Destroy(gameObject);
        else if (transform.position == startPosition) isReturnStart = false;
    }

    private void FixedUpdate()
    {
        if (isClicked && finalPosition == Vector3.zero)
        {
            Vector3 whichWay = carAxis == Axis.horizontal ? Vector3.forward : Vector3.left;
            speed = Math.Abs(speed);
            if (carDirectionX == Direction.Left || carDirectionY == Direction.Bottom)
                speed *= -1;
            rb.MovePosition(rb.position + whichWay * speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Car") || other.CompareTag("Barrier"))
        {
            isClicked = false;
            isReturnStart = true;
        }

    }

    public void SetFinalPos(Vector3 vector3)
    {
        finalPosition = vector3;
    }
}
