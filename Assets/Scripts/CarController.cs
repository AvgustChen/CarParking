using System;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public Text CountMoves;
    private Rigidbody rb;
    [SerializeField] private float speed = 5f, finalSpeed = 15f, rotationSpeed = 50f;
    private bool isClicked;
    private Vector3 finalPosition;
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

    private static int countCars = 0;

    private Direction carDirectionX = Direction.None, carDirectionY = Direction.None;

    private void Awake()
    {
        countCars++;
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        if (StartGame.isGameStarted)
        {
            curPointX = Input.mousePosition.x;
            curPointY = Input.mousePosition.y;
        }
    }

    private void OnMouseUp()
    {
        if (StartGame.isGameStarted)
        {
            isClicked = true;
            if (Input.mousePosition.x - curPointX > 0 && carAxis == Axis.horizontal)
                carDirectionX = Direction.Right;
            else if (Input.mousePosition.x - curPointX < 0 && carAxis == Axis.horizontal)
                carDirectionX = Direction.Left;

            if (Input.mousePosition.y - curPointY > 0 && carAxis == Axis.vertical)
                carDirectionY = Direction.Top;
            else if (Input.mousePosition.y - curPointY < 0 && carAxis == Axis.vertical)
                carDirectionY = Direction.Bottom;

            CountMoves.text = (Convert.ToInt32(CountMoves.text) - 1).ToString();

        }
    }

    private void Update()
    {
        if (CountMoves.text == "0" && countCars > 0 && !isClicked)
        {
            StartGame.Instance.LoseGame();
        }

        if (finalPosition != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPosition, finalSpeed * Time.deltaTime);

            Vector3 lookAtPos = finalPosition - transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookAtPos), rotationSpeed * Time.deltaTime);
        }

        if (transform.position == finalPosition)
        {
            countCars--;
            Destroy(gameObject);
        }
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Car") || other.CompareTag("Barrier"))
        {

            if (carAxis == Axis.horizontal && isClicked)
            {
                float adding = carDirectionX == Direction.Left ? 0.5f : -0.5f;
                transform.position = new Vector3(transform.position.x, 0, transform.position.z + adding);
            }

            if (carAxis == Axis.vertical && isClicked)
            {
                float adding = carDirectionY == Direction.Top ? 0.5f : -0.5f;
                transform.position = new Vector3(transform.position.x + adding, 0, transform.position.z);
            }
            isClicked = false;
        }

    }

    public void SetFinalPos(Vector3 vector3)
    {
        finalPosition = vector3;
    }
}
