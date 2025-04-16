using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool isGameStarted = false;
    private GameManagerUI gameManagerUI;
    [SerializeField] private GameObject[] levelsList;
    private int level;
    [SerializeField] private Transform levelLoadPoint;

    private void Awake()
    {
        Instance = this;
        gameManagerUI = GetComponent<GameManagerUI>();
        level = YG2.saves.level;
        if(level < levelsList.Length)
            Instantiate(levelsList[level], levelLoadPoint);
        else 
        {
            Instantiate(levelsList[Random.Range(0, levelsList.Length)], levelLoadPoint);
        }
    }

    private void Start()
    {
        CarsController.Instance.FindCarsInLevel();
        HumansManager.Instance.CreateHumans();
        gameManagerUI.PeopleCountText();
    }

    public void HumanSeat()
    {
        gameManagerUI.PeopleCountText();
    }

    public void PlayGame()
    {
        isGameStarted = true;
        gameManagerUI.StartGame();
    }

    public void LoadNextGame()
    {
        SceneManager.LoadScene(0);
    }

    public void LoseGame()
    {
        isGameStarted = false;
        gameManagerUI.LoseGame();
    }

    public void WinGame()
    {
        isGameStarted = false;
        YG2.saves.level++;
        YG2.SaveProgress();
        isGameStarted = false;
        gameManagerUI.WinGame();
    }

    public bool GetIsGameStarted()
    {
        return isGameStarted;
    }
}
