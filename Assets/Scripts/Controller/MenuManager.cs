using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoSingleton<MenuManager>
{
    [Header("Panel Scenes")]
    public GameObject       MenuGamePanel;
    public GameObject       GameOverPanel;

    [Header("Menu")]
    public GameObject       MenuCanvas;
    public GameObject       GalleryPanel;
    public GameObject       HelpPanel;
    public GameObject       SettingsPanel;

    [Header("GameOver Canvas")]
    public Text             TxtScore;
    public Text             TxtHighScore;

    public void SceneToLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        if (sceneName.Equals("InGame"))
        {
            MenuCanvas.SetActive(false);
        }
        else
        {
            MenuCanvas.SetActive(true);
            StartCoroutine("ShowCanvas", sceneName);
        }
    }

    IEnumerator ShowCanvas(string sceneName)
    {
        yield return new WaitForSeconds(0.05f);

        if(sceneName.Equals("Menu"))
        {
            menuGame(true);
        }
        else
        {
            gameOver(true);
        }
    }

    private void UpdateCanvasGameOver()
    {
        if(GameManager.Instance.GetCurrentScore() > GameManager.Instance.GetHighScore())
        {
            GameManager.Instance.UpdateHighScore(GameManager.Instance.GetCurrentScore());

            TxtScore.text = $"NOVO RECORDE! \n{ GameManager.Instance.GetHighScore() }";

            TxtHighScore.text = "";
        }
        else
        {
            TxtScore.text = $"PONTUAÇÃO \n{ GameManager.Instance.GetCurrentScore() }";

            TxtHighScore.text = $"MELHOR PONTUAÇÃO: { GameManager.Instance.GetHighScore()}";
        }
    }

    public void menuGame(bool state)
    {
        MenuGamePanel.SetActive(state);
    }

    public void gameOver(bool state)
    {
        UpdateCanvasGameOver();
        GameOverPanel.SetActive(state);
    }

    public void galleryPanel(bool state)
    {
        GalleryPanel.SetActive(state);
    }

    public void helpInMenu(bool state)
    {
        HelpPanel.SetActive(state);
    }

    public void settingsPanel(bool state)
    {
        SettingsPanel.SetActive(state);
    }



}
