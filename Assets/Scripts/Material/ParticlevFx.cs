using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlevFx : MonoBehaviour
{
    private Rigidbody2D         fxRb;
    public float                speed;
    public float                lifeTime;

    private void Start() {

        if(Player.Instance.GetLayerSkin()==2 || Player.Instance.GetLayerSkin()==4)
        {
            fxRb = GetComponent<Rigidbody2D>();
            fxRb.velocity = new Vector2(speed, 0);
        }

        Destroy(this.gameObject, lifeTime);
    }


    private void OnParticleCollision(GameObject other) {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.transform.position = new Vector2(other.transform.position.x, other.transform.position.y + 0.5f);
            GameObject deathVfx = Instantiate(GameController.Instance.deathVfxPrefab, other.transform.position, other.transform.rotation);
            deathVfx.GetComponent<Rigidbody2D>().velocity = new Vector2(GameController.Instance.getSpeed(), 0);

            Destroy(other.gameObject);
            Destroy(deathVfx.gameObject, 0.5f);
        }
    }

}
