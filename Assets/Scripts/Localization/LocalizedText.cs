﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    public string   key;
    private Text    text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void OnGUI()
    {

        text.text = LocalizationManager.Instance.GetLocalizedValue(key);
        
    }

    

}
