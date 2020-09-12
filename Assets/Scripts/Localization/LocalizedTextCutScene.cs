using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedTextCutScene : MonoBehaviour
{
    public string key;

    void Start()
    {
        Text text = GetComponent<Text>();
        text.text = LocalizationManager.Instance.GetLocalizedValueCutScene(key);
    }
}
