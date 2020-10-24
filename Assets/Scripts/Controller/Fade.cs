using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private Animator fadeAnim;
    
    void Start()
    {
        fadeAnim = GetComponent<Animator>();
        //fadeAnim.SetTrigger("fadeIn");
        Invoke("DisableFade", 0.7f);
    }

    public void EnableFade()
    {
        gameObject.SetActive(true);
        //fadeAnim.SetTrigger("fadeIn");
        Invoke("DisableFade", 0.7f);
    }

    public void DisableFade()
    {
        gameObject.SetActive(false);
    }
}
