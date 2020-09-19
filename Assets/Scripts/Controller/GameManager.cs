using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private float           sfxVol;
    private float           soundVol;
    private float           masterVol;
    private int             highScore;
    private int             currentScore;
    private bool            isPlayerAlive;
    private string          language;

    public override void Init()
    {
        base.Init();
        //language = "language_pt-br.json";
        language = "language_en.json";
        currentScore = 0;
        isPlayerAlive = true;
    }

    public int GetSkinID()
    {
        if(PlayerPrefs.HasKey("skinId"))
        {
            return PlayerPrefs.GetInt("skinId");
        }
        return -1;                              //personagem aleatorio
    }

    public void UpdateSkinID(int value)
    {
        PlayerPrefs.SetInt("skinId", value);
    }

    public string GetLanguage()
    {
        if(PlayerPrefs.HasKey("language"))
        {
            return PlayerPrefs.GetString("language");
        }
        return language;
    }

    public void UpdateLanguage(string value)
    {
        language = value;
        PlayerPrefs.SetString("language", value);
    }

    public void SetStatusPlayer(bool status)
    {
        isPlayerAlive = status;
    }

    public bool GetStatusPlayer()
    {
        return isPlayerAlive;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void UpdateCurrentScore(int value)
    {
        currentScore = value;
    }

    public int GetIsFirstTime()
    {
        return PlayerPrefs.GetInt("isFirstTime");
    }
    
    public void UpdateFirstTime(int value)
    {
        PlayerPrefs.SetInt("isFirstTime", value);
    }

    public int GetHighScore()
    {
        if(PlayerPrefs.HasKey("highScore"))
        {
            return PlayerPrefs.GetInt("highScore");
        }
        return 0;
    }

    public void UpdateHighScore(int value)
    {
        PlayerPrefs.SetInt("highScore", value);
    }

    public float GetSfxVol()
    {
        if(PlayerPrefs.HasKey("sfxVol"))
        {
            return PlayerPrefs.GetFloat("sfxVol");
        }
        return 0.5f;
    }

    public void UpdateSfxVol(float value)
    {
        PlayerPrefs.SetFloat("sfxVol", value);
    }

    public float GetSoundVol()
    {
        if(PlayerPrefs.HasKey("soundVol"))
        {
            return PlayerPrefs.GetFloat("soundVol");
        }
        return 0.5f;
    }

    public void UpdateSoundVol(float value)
    {
        PlayerPrefs.SetFloat("soundVol", value);
    }

    public float GetMasterVol()
    {
        if(PlayerPrefs.HasKey("masterVol"))
        {
            return PlayerPrefs.GetFloat("masterVol");
        }
        return 0.5f;
    }

    public void UpdateMasterVol(float value)
    {
        PlayerPrefs.SetFloat("masterVol", value);
    }

}
