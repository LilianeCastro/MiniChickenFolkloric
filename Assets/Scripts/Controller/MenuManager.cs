using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [Header("Settings")]
    public GameObject       Fade;
    public Animator         fadeAnim;

    public void SceneToLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        
        Fade.SetActive(true);

        if (sceneName.Equals("InGame"))
        {
            MenuCanvas.SetActive(false);
        }
        else
        {
            MenuCanvas.SetActive(true);
            if(sceneName.Equals("Menu"))
            {
                menuGame(true);
                gameOver(false);
            }
            else
            {
                menuGame(false);
                gameOver(true);
            }
        }
    }

    public void fadeOut()
    {
        fadeAnim.SetTrigger("fadeOut");
    }

    public void menuGame(bool state)
    {
        MenuGamePanel.SetActive(state);
    }

    public void gameOver(bool state)
    {
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
