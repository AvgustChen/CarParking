using UnityEngine;

public class ParkingsStation : MonoBehaviour
{
    public static ParkingsStation Instance;
    [SerializeField] public Transform[] parkings;

    private void Start()
    {
        Instance = this;
    }
}
