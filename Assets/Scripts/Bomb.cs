using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Rigidbody2D bombRb;

    public characterSpecialAttack charName;
    public float force;
    public float lifeTime;

    void Start()
    {
        bombRb = GetComponent<Rigidbody2D>();
        switch(charName)
        {
            /*case characterSpecialAttack.Boto:
                bombRb.AddForce(Vector2.one * force, ForceMode2D.Impulse);
                break;
            case characterSpecialAttack.Boi:
                bombRb.AddForce(Vector2.one * force, ForceMode2D.Impulse);
                break;*/
            /*case characterSpecialAttack.Curupira:
                bombRb.velocity = new Vector2(force, 0);
                break;*/
            case characterSpecialAttack.Iara:
                bombRb.velocity = new Vector2(force, force);
                break;
            case characterSpecialAttack.Saci:
                break;
            case characterSpecialAttack.Vitoria:
                break;
            default:
                bombRb.AddForce(Vector2.one * force, ForceMode2D.Impulse);
                break;
        }


    }

    private void OnEnable() {
        Invoke("Explosion", lifeTime);
    }

    private void Explosion()
    {
        Instantiate(GameController.Instance.vFxExplosionPrefab[Player.Instance.getLayerSkin()], transform.position, transform.rotation);
        Destroy(this.gameObject);
    }



}
