﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum characterSpecialAttack{
    Boto, Boi, Curupira, Iara, Saci, Vitoria
}

public class GameController : MonoSingleton<GameController>
{
    [Header("HUD")]
    public Image		    imgAttack;
    public Image		    imgSpecialAttack;
    public Slider		    progressAttack;
    public Slider		    progressSpecialAttack;
    public Slider		    volume;
    public Text			    txtScore;
    public Animator         progressAttackAnim;
    public Animator         progressSpecialAttackAnim;
    public GameObject       pausePanel;

    [Header("Game Config")]
    public float		    speedGame;
    public float            increaseSpeedGame;
    public float            speedShot;
    public float            increaseSpeedShot;
    public float            speedEnemyShot;
    public float            increaseEnemySpeedShot;
    public int              scoreToChangeSpeedGame;
    
    private float           currentSpeed;
    private float           currentSpeedShot;
    private float           curretEnemySpeedShot;
    private int             score;
    private bool            isPaused;
    private bool            canPause;

    [Header("Prefabs")]
    public GameObject[]		collectableFeedbackPrefab;
    public GameObject		deathVfxPrefab;

    [Header("Prefab that depends on the skin")]
    public Image[]          imgHUDAttackPrefab;
    public Image[]          imgHUDSpecialAttackPrefab;
    public GameObject[]		specialAttackPrefab;
    public GameObject[]		vFxExplosionPrefab;
    public GameObject[]     attackEnemyRanged;

    private GameObject      platTemp;

    private void Start() 
    {
        currentSpeed = speedGame;
        currentSpeedShot = speedShot;
        curretEnemySpeedShot = speedEnemyShot;

        imgAttack.sprite = imgHUDAttackPrefab[Player.Instance.GetLayerSkin()].sprite;
        imgSpecialAttack.sprite = imgHUDSpecialAttackPrefab[Player.Instance.GetLayerSkin()].sprite;

        progressAttack.value = progressAttack.maxValue;
        progressSpecialAttack.value = progressSpecialAttack.maxValue;

        volume.value = SoundManager.Instance.GetAudioSourceVol();

        Invoke("StartSound", 0.01f);
        Invoke("CanPause", 1f);

    }

    private void StartSound()
    {
        SoundManager.Instance.ChangeSong("InGame");
        GameManager.Instance.UpdateFirstTime(1);
    }

    private void CanPause()
    {
        canPause = true;
    }

    public bool CanPauseNow()
    {
        return canPause;
    }

    private void FixedUpdate() {
        if(progressAttack.value < progressAttack.maxValue)
        {
            progressAttack.value += Time.deltaTime;
        }
        if(progressSpecialAttack.value < progressSpecialAttack.maxValue)
        {
            progressSpecialAttack.value += Time.deltaTime;
        }
    }

    public int getIdSkinPlayer()
    {
        return Player.Instance.GetLayerSkin();
    }

    public void updateScore(int value)
    {
        score += value;
        txtScore.text = score.ToString();
        setSpeed();
    }

    public int getScore()
    {
        return score;
    }

    public void updateProgressAttack(int value)
    {
        if(progressAttack.value != progressAttack.maxValue)
        {
            progressAttackAnim.SetTrigger("isPlus");
        }
        progressAttack.value += value;
    }

    public float getProgressAttackValue()
    {
        return progressAttack.value;
    }

    public void updateProgressSpecialAttack(int value)
    {
        if(progressSpecialAttack.value != progressSpecialAttack.maxValue)
        {
            progressSpecialAttackAnim.SetTrigger("isPlus");
        }
        progressSpecialAttack.value += value;
    }

    public float getProgressSpecialAttackValue()
    {
        return progressSpecialAttack.value;
    }

    public bool CanSpawnAbovePercent(int percent)
    {
        return Random.Range(0, 100) < percent;
    }

    public float getSpeed()
    {
        return currentSpeed;
    }

    public float getSpeedShot()
    {
        return currentSpeedShot;
    }

    public float getEnemySpeedShot()
    {
        return curretEnemySpeedShot;
    }

    private void setSpeed()
    {
        currentSpeed = speedGame + (increaseSpeedGame * (Mathf.Floor(getScore() / scoreToChangeSpeedGame)));
        currentSpeedShot = speedShot + (increaseSpeedShot * (Mathf.Floor(getScore() / scoreToChangeSpeedGame)));
        curretEnemySpeedShot = speedEnemyShot + (increaseEnemySpeedShot * (Mathf.Floor(getScore() / scoreToChangeSpeedGame)));
    }

    public void playFx(int idFx)
    {
        SoundManager.Instance.playFx(idFx);
    }

    public void stopFx()
    {
        SoundManager.Instance.StopFx();
    }

    public void GameOver()
    {
        currentSpeed = 0f;
 
        GameManager.Instance.SetStatusPlayer(false);
        SoundManager.Instance.StopMusic();
        GameController.Instance.playFx(5);

        StartCoroutine("GameOverChangeScene");  
    }

    IEnumerator GameOverChangeScene()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.UpdateCurrentScore(score);

        MenuManager.Instance.SceneToLoad("GameOver");
        SoundManager.Instance.ChangeSong("GameOver");
    }

    public void ChangeScene(string sceneName)
    {
        MenuManager.Instance.SceneToLoad(sceneName);
        SoundManager.Instance.ChangeSong(sceneName);
        if(isPaused)
        {
            Time.timeScale = 1f;
        }
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        GameManager.Instance.SetStatusPlayer(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.SetStatusPlayer(true);
    }

    public void Volume()
    {
        SoundManager.Instance.SetAudioSourceVol(volume.value);
    }

}
