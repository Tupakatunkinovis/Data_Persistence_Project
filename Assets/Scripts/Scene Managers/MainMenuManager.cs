using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScoreText, textField, ErrorText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Player bestPlayer = GameDAO.GetInstance().GetPlayer(0);
        if (bestPlayer.name == "")
        {
            bestScoreText.SetText("");
        }
        else
            bestScoreText.SetText("Best Score: " + bestPlayer.name + " - " + bestPlayer.score);


        textField.text = "";
    }

    public void StartGame()
    {
        if (textField.text.Length != 1)
        {
            GameDAO.GetInstance().currentPlayer.name = textField.text;
            GameDAO.GetInstance().currentPlayer.score = 0;

            SceneManager.LoadScene(1);
        }
        else
        {
            StartCoroutine(ShowMessageError());
        }
    }

    public void ScoresMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator ShowMessageError()
    {
        ErrorText.enabled = true;

        yield return new WaitForSeconds(3);

        ErrorText.enabled = false;
    }

}