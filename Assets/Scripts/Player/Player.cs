using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoSingleton<Player>
{
    private Rigidbody2D playerRb;
    private Animator playerAnim;
    private float initialPosX;

    [Header("GameObject Config")]
    public LayerMask layerColision;
    public Transform groundCheck;
    public Transform posSpawn;

    [Header("Player Config")]
    public float forceJump;
    private bool isGround;

    private void Start() {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        initialPosX = transform.position.x;
    }

    private void Update() {
        transform.position = new Vector2(initialPosX, transform.position.y);

        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.02f, layerColision);
    }

    public void Jump()
    {
        if(isGround)
        {
            playerRb.AddForce(new Vector2(0, forceJump));
        }
    }

    public void Fire()
    {
        GameObject obj = ObjectPooling.Instance.GetPooledObject();
        obj.TryGetComponent(out SpriteRenderer objSprite);
        objSprite.sprite = GameController.Instance.shotSprite[0];
        //objSprite.sprite = GameController.Instance.shotRenderer[0].shotSprite;

        obj.transform.position = posSpawn.position;
        obj.transform.rotation = posSpawn.rotation;
        obj.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Enemy")
        {
            print("Colidiu com inimigo collision");
        }
    }

}
