using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlevFx : MonoBehaviour
{
    private Rigidbody2D         fxRb;
    public float                speed;
    public float                lifeTime;

    private void Start() {

        if(Player.Instance.getLayerSkin()==2 || Player.Instance.getLayerSkin()==4)
        {
            fxRb = GetComponent<Rigidbody2D>();
            fxRb.velocity = new Vector2(speed, 0);
        }

        Destroy(this.gameObject, lifeTime);
    }


    private void OnParticleCollision(GameObject other) {
        if(other.gameObject.tag=="Enemy")
        {
            Destroy(other.gameObject);
        }
    }

}
