using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    void Start()
    {
        Invoke("DisableFade", 0.9f);
    }

    void DisableFade()
    {
        gameObject.SetActive(false);
    }
}
