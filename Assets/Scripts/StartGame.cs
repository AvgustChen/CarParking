using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public static StartGame Instance;
    public static bool isGameStarted = false;
    public GameObject logo, playButton, countMoves, loseText;
    private bool isLoseGame = false;

    private void Start()
    {
        Instance = this;
    }
    public void PlayGame()
    {
        if (!isLoseGame)
        {
            isGameStarted = true;
            logo.SetActive(false);
            playButton.SetActive(false);
            countMoves.SetActive(true);
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
        countMoves.SetActive(false);
        loseText.SetActive(true);
    }
}
