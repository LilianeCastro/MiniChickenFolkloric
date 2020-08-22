using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuca : MonoBehaviour
{
    private Rigidbody2D cucaRb;

    void Start()
    {
        cucaRb = GetComponent<Rigidbody2D>();

        ExitScreen();
    }

    void ExitScreen()
    {
        cucaRb.velocity = Vector2.right * 0.9f;
        Invoke("Destroy", 5f);
    }

    void Destroy()
    {
        this.gameObject.SetActive(false);
    }

   
}
