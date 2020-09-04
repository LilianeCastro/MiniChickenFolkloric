using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    void Start()
    {
        Invoke("DisableFade", 0.7f);
    }

    public void EnableFade()
    {
        gameObject.SetActive(true);
        Invoke("DisableFade", 0.7f);
    }

    public void DisableFade()
    {
        gameObject.SetActive(false);
    }
}
