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
    private bool isPlatform;
    private int chosenSkinLayer;

    private void Start() {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        layerSkin();

        initialPosX = transform.position.x;
    }

    private void Update() {
        transform.position = new Vector2(initialPosX, transform.position.y);

        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.02f, layerColision);

        playerAnim.SetBool("isGround", isGround);
    }

    private void layerSkin()
    {
        chosenSkinLayer = 2;//Random.Range(0,playerAnim.layerCount);
        playerAnim.SetLayerWeight(chosenSkinLayer, 1);
        print(chosenSkinLayer);
    }

    public int getLayerSkin()
    {
        return chosenSkinLayer;
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
        playerAnim.SetTrigger("attack");
        GameObject obj = ObjectPooling.Instance.GetPooledObject();

        obj.transform.position = posSpawn.position;
        obj.transform.rotation = posSpawn.rotation;
        obj.SetActive(true);
    }

    public void Bomb()
    {
        playerAnim.SetTrigger("bomb");
        Instantiate(GameController.Instance.specialAttackPrefab[chosenSkinLayer], posSpawn.position, posSpawn.rotation);

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Enemy")
        {
            playerAnim.SetTrigger("death");
            print("Colidiu com inimigo collision");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Collectable")
        {
            print("Colidiu com coletavel");
        }
    }

}
