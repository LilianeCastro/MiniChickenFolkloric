using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoSingleton<Player>
{
    private Rigidbody2D         playerRb;
    private Animator            playerAnim;
    private float               initialPosX;

    private Collectable         collec;
    private GameObject          objPolled;

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

    public override void Init()
    {
        base.Init();

        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();

        playerAnim.SetLayerWeight(0, 0);

        layerSkin();
    }

    private void Start() 
    {
        if(getLayerSkin()==3)
        {
            vfxPlayer.SetActive(true);
        }

    }

    private void Update()
    {

        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.02f, layerColision);

        playerAnim.SetBool("isGround", isGround);
    }

    private void layerSkin()
    {
        chosenSkinLayer = Random.Range(0,playerAnim.layerCount);
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
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(new Vector2(0, forceJump));
        }
    }

    public void Fire()
    {
        if(!canShot && GameController.Instance.getProgressAttackValue()>=30)
        {
            GameController.Instance.updateProgressAttack(-30);

            playerAnim.SetTrigger("attack");
            objPolled = ObjectPooling.Instance.GetPooledObject();

            objPolled.transform.position = posSpawn.position;
            objPolled.transform.rotation = posSpawn.rotation;
            objPolled.SetActive(true);
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

    private void OnTriggerEnter2D(Collider2D other) 
    {

        switch(other.gameObject.tag)
        {
            case "Collectable":

                collec = other.GetComponent<Collectable>();
                if(collec.idCollectable.Equals("egg"))
                {
                    GameController.Instance.updateProgressAttack(10);
                }
                else
                {
                    GameController.Instance.updateProgressSpecialAttack(20);
                }

                GameController.Instance.playFx(0);
                GameController.Instance.updateScore(1);
                Destroy(other.gameObject);
                break;

            case "Enemy":

                playerAnim.SetTrigger("death");
                GameController.Instance.GameOver();
                break;

            case "WaterDamage":

                if(!getLayerSkin().Equals(3))
                {
                    playerAnim.SetTrigger("death");
                    GameController.Instance.GameOver();
                }
                else
                {
                    GameController.Instance.updateProgressAttack(5);
                    GameController.Instance.updateProgressSpecialAttack(10);
                }
                break;

        }
    }

    IEnumerator DelayShot()
    {
        yield return new WaitForSeconds(delayShot);

        canShot = false;
    }

}
