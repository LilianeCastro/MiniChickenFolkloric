using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void SceneToLoad()
    {
        SceneManager.LoadScene("InGame");
        GameManager.Instance.UpdateFirstTime(1);
    }
}
