using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Shot")
        {
            print("Colidiu");
            Destroy(this.gameObject);
        }

        if(other.tag=="Player")
        {
            print("Colidiu com player trigger");
        }
    }
}
