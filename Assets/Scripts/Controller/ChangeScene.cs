using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string nameScene = "InGame";
    public void SceneToLoad()
    {
        SceneManager.LoadScene(nameScene);
    }
}
