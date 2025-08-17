using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject rankingBoardContent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreBoard();
    }

    private void UpdateScoreBoard()
    {

        TextMeshProUGUI[] textFields = rankingBoardContent.GetComponentsInChildren<TextMeshProUGUI>();

        for (int i = 0; i < textFields.Length; i++)
        {
            Debug.Log("Getting Player " + i);
            Player player = GameDAO.GetInstance().GetPlayer(i);
            if (player.name == "")
            {
                textFields[i].text = "-";
            }
            else
            {
                textFields[i].text = i+1 + ". " + player.name + " - " + player.score;
            }
        }

    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
