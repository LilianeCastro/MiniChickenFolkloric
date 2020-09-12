using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;


public class LocalizationManager : MonoSingleton<LocalizationManager>
{
    private Dictionary<string, string> localizedText;
    private Dictionary<string, string> localizedTextCutScene;

    private bool isReady;
    private string missingTextString = "Localized text not found";

    private string dataAsJson;

    public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        localizedTextCutScene = new Dictionary<string, string>();

        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if(filePath.Contains("://") || filePath.Contains(":///"))
        {
            StartCoroutine(GetRequest(filePath));
        }
        else
        {
            dataAsJson = File.ReadAllText(filePath);
            LoadedData();
        }

        isReady = true;
            
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError("ErroConexao");
            }
            else
            {
                dataAsJson = webRequest.downloadHandler.text;

                LoadedData();
                
            }
        }
    }

    private void LoadedData()
    {
        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

        for(int i = 0; i < loadedData.items.Length; i++)
        {
            localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }

        for(int i = 0; i < loadedData.cutscene.Length; i++)
        {
            localizedTextCutScene.Add(loadedData.cutscene[i].key, loadedData.cutscene[i].value);
        }

        Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
        Debug.Log("Data loaded, dictionary contains: " + localizedTextCutScene.Count + " entries");
    }

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;

        if(localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }

        return  result;
    }

    public string GetLocalizedValueCutScene(string key)
    {
        string result = missingTextString;

        if(localizedTextCutScene.ContainsKey(key))
        {
            result = localizedTextCutScene[key];
        }

        return  result;
    }

    public bool GetIsReady()
    {
        return isReady;
    }
}
