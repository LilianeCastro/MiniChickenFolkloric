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
    public Text             TxtRecord;

    [Header("GameOver Canvas")]
    public Image[]          spriteGameOverSkins;
    public Image            spriteGameOver;
    public Text             TxtScore;
    public Text             TxtHighScore;
    public Text             score; 
    public Text             highScore;
    public Text             newHighScore;
    public Color            colorDefault;

    [Header("Settings")]
    public Button[]         btnChosenLanguage;
    public Image[]          imgLanguage;
    public Image[]          imgLanguageCheck;
    public Image[]          spriteVol;
    public Image            settingsSpriteVol;
    public Slider           soundVolume;
    public GameObject       spriteCharMenu;

    private void Start()
    {
        if(GameManager.Instance.GetLanguage().Equals("language_pt-br.json"))
        {
            UpdateImageLanguage(0);
        }
        else
        {
            UpdateImageLanguage(1);
        }

        TxtRecord.text = GameManager.Instance.GetHighScore().ToString();
    }

    public void SceneToLoad(string sceneName)
    {
        GameManager.Instance.SetStatusPlayer(true);

        if(sceneName.Equals("GameOver"))
        {
            if(GameManager.Instance.GetHighScore() >= GameManager.Instance.GetCurrentScore())
            { 
                highScore.gameObject.SetActive(true);
                newHighScore.gameObject.SetActive(false);
                score.color = colorDefault;

                spriteGameOver.sprite = spriteGameOverSkins[Player.Instance.getLayerSkin()].sprite;
            }
            else
            {
                highScore.gameObject.SetActive(false);
                newHighScore.gameObject.SetActive(true);
                score.color = Color.clear;

                spriteGameOver.sprite = spriteGameOverSkins[spriteGameOverSkins.Length-1].sprite;
            }
            
        }
        
        if(sceneName.Equals("InGame") && GameManager.Instance.GetIsFirstTime()==0)
        {
            SceneManager.LoadScene("CutScene");
            SoundManager.Instance.ChangeSong("CutScene");
        }
        else 
        {
            if(sceneName.Equals("InGame"))
            {
                MenuCanvas.SetActive(false);
            }
            else
            {
                MenuCanvas.SetActive(true);
                StartCoroutine("ShowCanvas", sceneName);   
            }

            if(sceneName.Equals("Menu"))
            {
                spriteCharMenu.SetActive(true);
                TxtRecord.text = GameManager.Instance.GetHighScore().ToString();
            }
        
            SceneManager.LoadScene(sceneName);
            _Fade.EnableFade();
        }

        

        /*if (sceneName.Equals("InGame"))
        {
            MenuCanvas.SetActive(false);
        }
        else
        {
            MenuCanvas.SetActive(true);
            StartCoroutine("ShowCanvas", sceneName);
        }

        if(sceneName.Equals("Menu"))
        {
            TxtRecord.text = GameManager.Instance.GetHighScore().ToString();
        }*/
    }

    IEnumerator ShowCanvas(string sceneName)
    {
        yield return new WaitForSeconds(0.05f);

        if(sceneName.Equals("Menu"))
        {
            MenuGame(true);
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

            TxtScore.text = $"{ GameManager.Instance.GetCurrentScore() }";

            TxtHighScore.text = $"{ GameManager.Instance.GetHighScore()}";
        }
        else
        {

            GameManager.Instance.UpdateHighScore(GameManager.Instance.GetCurrentScore());

            TxtScore.text = $"{ GameManager.Instance.GetHighScore() }";

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
        TxtRecord.text = GameManager.Instance.GetHighScore().ToString();
    }

    public void UpdateImageLanguage(int pos)
    {
        for(int i = 0; i < imgLanguage.Length; i ++)
        {
            btnChosenLanguage[i].image.sprite = imgLanguage[i].sprite;
        }

        btnChosenLanguage[pos].image.sprite = imgLanguageCheck[pos].sprite;
        
    }

    public void MenuGame(bool state)
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

    public void ExitGame()
    {
        Application.Quit();
    }

}
