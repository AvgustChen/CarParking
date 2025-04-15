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
    }

    public void HumanSeat()
    {
        imageProgress.fillAmount = (float)car.countPass / (float)car.numberOfSeats;
        audioSource.clip = clickSound;
        audioSource.volume = 0.3f;
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
}
