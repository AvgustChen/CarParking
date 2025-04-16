using TMPro;
using UnityEngine;

public class GameManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private TMP_Text peopleCount;

    public void PeopleCountText()
    {
        peopleCount.text = HumansManager.Instance.humansList.Count.ToString();
    }

    public void WinGame()
    {
        winPanel.SetActive(true);
    }

    public void LoseGame()
    {
        losePanel.SetActive(true);
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
    }
}
