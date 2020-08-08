using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoSingleton<Player>
{
    private Rigidbody2D         playerRb;
    private Animator            playerAnim;
    private float               initialPosX;

    [Header("GameObject Config")]
    public LayerMask            layerColision;
    public Transform            groundCheck;
    public Transform            posSpawn;
    public GameObject           vfxPlayer;

    [Header("Player Config")]
    public float                forceJump;
    public float                delayShot;
    private bool                isGround;
    private bool                isPlatform;
    private bool                canShot;
    private int                 chosenSkinLayer;

    private void Start() {

        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();

        playerAnim.SetLayerWeight(0, 0);
        layerSkin();

        if(getLayerSkin()==3)
        {
            vfxPlayer.SetActive(true);
        }

        initialPosX = transform.position.x;
    }

    private void Update() {
        transform.position = new Vector2(initialPosX, transform.position.y);

        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.02f, layerColision);

        playerAnim.SetBool("isGround", isGround);
    }

    private void layerSkin()
    {
        chosenSkinLayer = Random.Range(0,playerAnim.layerCount);
        playerAnim.SetLayerWeight(chosenSkinLayer, 1);
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
        if(!canShot && GameController.Instance.getProgressAttackValue()>=30)
        {
            GameController.Instance.updateProgressAttack(-30);

            playerAnim.SetTrigger("attack");
            GameObject obj = ObjectPooling.Instance.GetPooledObject();

            obj.transform.position = posSpawn.position;
            obj.transform.rotation = posSpawn.rotation;
            obj.SetActive(true);
            canShot = true;

            StartCoroutine("DelayShot");
        }

    }

    public void Bomb()
    {
        if(GameController.Instance.getProgressSpecialAttackValue()>=100)
        {
            GameController.Instance.updateProgressSpecialAttack(-100);

            playerAnim.SetTrigger("bomb");
            Instantiate(GameController.Instance.specialAttackPrefab[chosenSkinLayer], posSpawn.position, posSpawn.rotation);
        }
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
            Collectable collec = other.GetComponent<Collectable>();
            if(collec.idCollectable == "egg")
            {
                GameController.Instance.updateProgressAttack(10);
            }
            else
            {
                GameController.Instance.updateProgressSpecialAttack(20);
            }

            GameController.Instance.updateScore(1);
            Destroy(other.gameObject);
        }
    }

    IEnumerator DelayShot()
    {
        yield return new WaitForSeconds(delayShot);

        canShot = false;
    }

}
