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
    private Dictionary<string, string> localizedTextGallery;

    private bool isReady;
    private string missingTextString = "Localized text not found";

    private string dataAsJson;

    public void LoadLocalizedText(string fileName)
    {
        isReady = false;

        localizedText = new Dictionary<string, string>();
        localizedTextCutScene = new Dictionary<string, string>();
        localizedTextGallery = new Dictionary<string, string>();

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

        for(int i = 0; i < loadedData.gallery.Length; i++)
        {
            localizedTextGallery.Add(loadedData.gallery[i].key, loadedData.gallery[i].value);
        }

        Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
        Debug.Log("Data loaded, dictionary contains: " + localizedTextCutScene.Count + " entries");
        Debug.Log("Data loaded, dictionary contains: " + localizedTextGallery.Count + " entries");


        isReady = true;
    }

    public string GetLocalizedValue(string key)
    {
        string result = "";

        if(localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }

        Debug.Log(missingTextString);
        return  result;
    }

    public string GetLocalizedValueCutScene(string key)
    {
        string result = "";

        if(localizedTextCutScene.ContainsKey(key))
        {
            result = localizedTextCutScene[key];
        }
        Debug.Log(missingTextString);
        return  result;
    }

    public string GetLocalizedValueGallery(string key)
    {
        string result = "";

        if(localizedTextGallery.ContainsKey(key))
        {
            result = localizedTextGallery[key];
        }
        Debug.Log(missingTextString);
        return  result;
    }

    public bool GetIsReady()
    {
        return isReady;
    }
}
