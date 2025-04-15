using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public static StartGame Instance;
    public static bool isGameStarted = false;
    public GameObject logo, playButton, loseText;
    public TMP_Text peopleCount;
    private bool isLoseGame = false;

    private void Start()
    {
        Instance = this;
        peopleCount.text = HumansManager.Instance.humansList.Count.ToString();

    }

    public void HumanSeat()
    {
        peopleCount.text = HumansManager.Instance.humansList.Count.ToString();
        
    }

    public void PlayGame()
    {
        if (!isLoseGame)
        {
            isGameStarted = true;
            logo.SetActive(false);
            playButton.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void LoseGame()
    {
        isLoseGame = true;
        isGameStarted = false;
        logo.SetActive(true);
        playButton.SetActive(true);
        loseText.SetActive(true);
    }
}
