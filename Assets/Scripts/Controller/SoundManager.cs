using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource audioSource;

    public AudioClip menuSound;

    public AudioClip[] inGameSound;
    public AudioClip[] fx;

    public void changeSong(string sceneName)
    {
        audioSource.Stop();
        
        if(sceneName.Equals("InGame"))
        {
            audioSource.clip = inGameSound[GameController.Instance.getIdSkinPlayer()];
        }
        else
        {
            audioSource.clip = menuSound;
        }

        audioSource.Play();
    }

    public void playFx(int idFx)
    {
        switch(idFx)
        {
            // Player jump
            case 0:
                audioSource.PlayOneShot(fx[idFx]);
                break;
            // Egg collected
            case 1:
                audioSource.PlayOneShot(fx[idFx]);
                break;
            // Chick collected
            case 2:
                audioSource.PlayOneShot(fx[idFx]);
                break;
            // Player death
            case 3:
                audioSource.PlayOneShot(fx[idFx]);
                break;
            // Kunai hit
            case 4:
                audioSource.PlayOneShot(fx[idFx]);
                break;
            // Bomb explosion
            case 5:
                audioSource.PlayOneShot(fx[idFx]);
                break;
            // Highscore
            case 6:
                audioSource.PlayOneShot(fx[idFx]);
                break;
            // Game Over
            case 7:
                audioSource.PlayOneShot(fx[idFx]);
                break;
            default:
                audioSource.clip = default;
                break;
        }
    }

    public float getAudioSourceVol()
    {
        return audioSource.volume;
    }

    public void setAudioSourceVol(float newVol)
    {
        audioSource.volume = newVol;
    }
}
