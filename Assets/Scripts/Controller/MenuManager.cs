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
    public Fade            _Fade;

    [Header("Menu")]
    public GameObject       MenuCanvas;
    public GameObject       GalleryPanel;
    public GameObject       HelpPanel;
    public GameObject       SettingsPanel;

    [Header("GameOver Canvas")]
    public Image[]          spriteGameOverSkins;
    public Image            spriteGameOver;
    public Text             TxtScore;
    public Text             TxtHighScore;

    [Header("Settings")]
    public Image[]          spriteVol;
    public Image            settingsSpriteVol;
    public Slider           soundVolume;

    public void SceneToLoad(string sceneName)
    {
        GameManager.Instance.SetStatusPlayer(true);
        _Fade.EnableFade();

        if(sceneName.Equals("GameOver"))
        {
            if(GameManager.Instance.GetHighScore() >= GameManager.Instance.GetCurrentScore())
            {
                spriteGameOver.sprite = spriteGameOverSkins[Player.Instance.getLayerSkin()].sprite;
            }
            else
            {
                spriteGameOver.sprite = spriteGameOverSkins[spriteGameOverSkins.Length-1].sprite;
            }
            
        }
        
        if(sceneName.Equals("InGame") && GameManager.Instance.GetIsFirstTime()==0)
        {
            SceneManager.LoadScene("CutScene");
            SoundManager.Instance.changeSong("CutScene");
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
        

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
        if(GameManager.Instance.GetCurrentScore() <= GameManager.Instance.GetHighScore())
        {
//            spriteGameOver.sprite = spriteGameOverSkins[Player.Instance.getLayerSkin()].sprite;

            TxtScore.text = $"PONTUAÇÃO \n{ GameManager.Instance.GetCurrentScore() }";

            TxtHighScore.text = $"MELHOR PONTUAÇÃO: { GameManager.Instance.GetHighScore()}";
        }
        else
        {
//            spriteGameOver.sprite = spriteGameOverSkins[spriteGameOverSkins.Length-1].sprite;

            GameManager.Instance.UpdateHighScore(GameManager.Instance.GetCurrentScore());

            TxtScore.text = $"NOVO RECORDE! \n{ GameManager.Instance.GetHighScore() }";

            TxtHighScore.text = "";
        }
    }

    public void ActivateSettings()
    {
        CurrentImgSound();
        soundVolume.value = GameManager.Instance.GetMasterVol();
        StartCoroutine("SettingsConfig");
    }

    public void DisableSettings()
    {
        SoundManager.Instance.SetAudioSourceVol(SoundManager.Instance.GetAudioSourceVol());
        StopCoroutine("SettingsConfig");
    }

    IEnumerator SettingsConfig()
    {
        yield return new WaitForSeconds(0.1f);

        CurrentImgSound();
        SoundManager.Instance.SetAudioSourceVol(soundVolume.value);
        StartCoroutine("SettingsConfig");
    }

    void CurrentImgSound()
    {
        if(SoundManager.Instance.GetAudioSourceVol() > 0)
        {
            settingsSpriteVol.sprite = spriteVol[0].sprite;
        }
        else
        {
            settingsSpriteVol.sprite = spriteVol[1].sprite;
        }
    }

    public void ClearHighScore()
    {
        GameManager.Instance.UpdateHighScore(0);
        GameManager.Instance.UpdateFirstTime(0);
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
