using System;
using UnityEngine;
using UnityEngine.UI;

public class CarUI : MonoBehaviour
{
    [SerializeField] private Image imageProgress;
    [SerializeField] private AudioClip startMoveSound;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip clickSound;
    AudioSource audioSource;
    private Car car;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        car = GetComponent<Car>();
        imageProgress.fillAmount = 0f;
        ParkingsStation.Instance.OnHumanSeat += ParkingsStation_OnHumanSeat;
    }

    private void ParkingsStation_OnHumanSeat(object sender, EventArgs e)
    {
        imageProgress.fillAmount = (float)car.countPass / (float)car.numberOfSeats;
        audioSource.clip = clickSound;
        audioSource.Play();
    }

    public void StartMoveSound()
    {
        audioSource.clip = startMoveSound;
        audioSource.Play();
    }

    public void CrashSound()
    {
        audioSource.clip = crashSound;
        audioSource.Play();
    }

    private void OnDestroy()
    {
           ParkingsStation.Instance.OnHumanSeat -= ParkingsStation_OnHumanSeat;     
    }
}
