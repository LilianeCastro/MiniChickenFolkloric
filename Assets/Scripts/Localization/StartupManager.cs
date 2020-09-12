using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour
{
    public GameObject canvas;

    private void Start()
    {
        LocalizationManager.Instance.LoadLocalizedText(GameManager.Instance.GetLanguage());
        StartCoroutine("StartVerification");
    }

    private IEnumerator StartVerification()
    {
        while(!LocalizationManager.Instance.GetIsReady())
        {
            yield return null;
        }

        canvas.SetActive(true);
    }
}
