using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int             highScore;
    private float           sfxVol;
    private float           soundVol;
    private float           masterVol;

    public int getHighScore()
    {
        if(PlayerPrefs.HasKey("highScore"))
        {
            return PlayerPrefs.GetInt("highScore");
        }
        return 0;
    }

    public float getSfxVol()
    {
        if(PlayerPrefs.HasKey("sfxVol"))
        {
            return PlayerPrefs.GetInt("sfxVol");
        }
        return 0.5f;
    }

    public float getSoundVol()
    {
        if(PlayerPrefs.HasKey("soundVol"))
        {
            return PlayerPrefs.GetInt("soundVol");
        }
        return 0.5f;
    }

    public float getMasterVol()
    {
        if(PlayerPrefs.HasKey("masterVol"))
        {
            return PlayerPrefs.GetInt("masterVol");
        }
        return 0.5f;
    }

}
