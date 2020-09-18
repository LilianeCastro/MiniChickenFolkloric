using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private Animator fadeAnim;
    void Start()
    {
        Invoke("DisableFade", 0.5f);
    }

    public void EnableFade()
    {
        gameObject.SetActive(true);
        Invoke("DisableFade", 0.5f);
    }

    public void DisableFade()
    {
        gameObject.SetActive(false);
    }
}
