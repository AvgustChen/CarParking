using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HumansManager : MonoBehaviour
{
    public static HumansManager Instance;
    public List<Human> humansList;
    [SerializeField] private List<Human> humansType;
    [SerializeField] private Transform steckPoint;

    private void Start()
    {
        Instance = this;
        foreach (Car car in CarsController.Instance.carsList)
        {
            for (int i = 0; i < car.numberOfSeats; i++)
            {
                foreach (Human human in humansType)
                {
                    if (car.GetColor() == human.GetColor())
                    {
                        Human h = Instantiate(human, steckPoint);
                        humansList.Add(h);
                    }
                }
            }
        }
        //ShuffleHumans(humansList);
        SteckHumans();
    }

    private void ShuffleHumans(List<Human> humans)
    {
        for (int i = 0; i < humans.Count; i++)
        {
            Human temp = humans[i];
            int randomIndex = Random.Range(i, humans.Count);
            humans[i] = humans[randomIndex];
            humans[randomIndex] = temp;
        }
    }

    private void SteckHumans()
    {
        float offset = 1.5f;
        foreach (Human human in humansList)
        {
            human.transform.position = steckPoint.position;
            steckPoint.position -= new Vector3(offset, 0, 0);
            human.gameObject.transform.SetParent(gameObject.transform);
        }
    }
    public void OneStepSteckHumans()
    {
        float offset = 1.5f;
        foreach (Human human in humansList)
        {
            //human.transform.position += new Vector3(offset, 0, 0);

            human.transform.DOMoveX(human.transform.position.x + offset, 0.1f);
        }
    }
}
