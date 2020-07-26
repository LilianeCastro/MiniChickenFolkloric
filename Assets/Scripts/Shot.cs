using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private Rigidbody2D shotRb;
    public float speedShot;

    private void OnEnable() {
        shotRb = GetComponent<Rigidbody2D>();
        shotRb.velocity = new Vector2(speedShot, 0);
        Invoke("Destroy", 3f);
    }


    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Enemy")
        {
            print("shot colidiu com enemy");
            Destroy();
        }
    }
}
