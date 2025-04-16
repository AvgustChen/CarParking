using UnityEngine;

public class Parking : MonoBehaviour
{
    public bool isFree;

    private void Start()
    {
        isFree = true;
    }

    public bool GetIsFree()
    {
        return isFree;
    }

    public void SetIsFree(bool value)
    {
        isFree = value;
    }
}
