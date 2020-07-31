using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoSingleton<MenuManager>
{
    [Header("Panel Scenes")]
    public GameObject MenuGamePanel;
    public GameObject InGamePanel;
    public GameObject GameOverPanel;

    [Header("Menu")]
    public GameObject GalleryPanel;
    public GameObject HelpPanel;
    public GameObject SettingsPanel;

    [Header("Settings")]
    public Animator fadeAnim;

    public void fadeIn() {
        fadeAnim.SetTrigger("fadeIn");
    }

    public void fadeOut()
    {
        fadeAnim.SetTrigger("fadeOut");
    }

    public void menuGame(bool state)
    {
        MenuGamePanel.SetActive(state);
    }

    public void inGame(bool state)
    {
        InGamePanel.SetActive(state);
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
