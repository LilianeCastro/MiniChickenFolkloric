using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlevFx : MonoBehaviour
{
    private ParticleSystem ps;
    private Rigidbody2D fxRb;
    public float speed;

    private void Start() {

        ps = GetComponent<ParticleSystem>();

        if(Player.Instance.getLayerSkin()==2)
        {
            fxRb = GetComponent<Rigidbody2D>();
            fxRb.velocity = new Vector2(speed, 0);
        }
        //var main = ps.main;

        //Duration
        /*ps.Stop();

        main.duration = 10.0f;

        ps.Play();*/


    }
    private void OnParticleCollision(GameObject other) {
        if(other.gameObject.tag=="Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
