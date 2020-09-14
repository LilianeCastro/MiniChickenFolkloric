using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource audioSource;
    public AudioSource audioSourceSfx;

    public AudioClip menuSound;
    public AudioClip cutScene;

    public AudioClip[] inGameSound;
    public AudioClip[] fx;

    private void Start()
    {
        audioSource.volume = GameManager.Instance.GetMasterVol();
        audioSourceSfx.volume = GameManager.Instance.GetSfxVol();
    }

    public void changeSong(string sceneName)
    {
        audioSource.Stop();

        if(sceneName.Equals("InGame"))
        {
            audioSource.clip = inGameSound[GameController.Instance.getIdSkinPlayer()];
        }
        else if(sceneName.Equals("CutScene"))
        {
            audioSource.clip = cutScene;
        }
        else
        {
            audioSource.clip = menuSound;
        }

        audioSource.Play();
    }

    public void playFx(int idFx)
    {
        audioSourceSfx.PlayOneShot(fx[idFx]);
    }

    public void StopFx()
    {
        audioSourceSfx.Stop();
    }

    public float GetAudioSourceVol()
    {
        return GameManager.Instance.GetMasterVol();
    }

    //To Do: Sfx Vol
    public void SetAudioSourceVol(float newVol)
    {
        audioSource.volume = newVol;
        GameManager.Instance.UpdateMasterVol(newVol);
        
        SetAudioSourceSfxVol(newVol/2);
    }

    public float GetAudioSourceSfxVol()
    {
        return GameManager.Instance.GetSfxVol();
    }

    public void SetAudioSourceSfxVol(float newVol)
    {
        audioSourceSfx.volume = newVol;
        GameManager.Instance.UpdateSfxVol(newVol);
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

}
