using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private string color;

    public string GetColor()
    {
        return color;
    }

}
