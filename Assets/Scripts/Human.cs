using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] public SOColor soColor;

    public SOColor GetSOColor()
    {
        return soColor;
    }
}
