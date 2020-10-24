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
    private bool                isAlive;
    private int                 chosenSkinLayer;

    private int                 animIdGround;
    private int                 animIdAttack;
    private int                 animIdBomb;
    private int                 animIdDeath;

    public override void Init()
    {
        base.Init();

        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();

        playerAnim.SetLayerWeight(0, 0);

        animIdGround = Animator.StringToHash("isGround");
        animIdAttack = Animator.StringToHash("attack");
        animIdBomb = Animator.StringToHash("bomb");
        animIdDeath = Animator.StringToHash("death");

        layerSkin();

        isAlive = true;
    }

    private void Start() 
    {
        initialPosX = transform.localPosition.x;

        if(GetLayerSkin()==3)
        {
            vfxPlayer.SetActive(true);
        }
    }

    void FixedUpdate() {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.02f, layerColision);
    }

    private void Update()
    {
        transform.localPosition = new Vector3(initialPosX, transform.localPosition.y, transform.localPosition.z);
        
        playerAnim.SetBool(animIdGround, isGround);
    }

    private void layerSkin()
    {
        if(GameManager.Instance.GetSkinID()==-1)
        {
            chosenSkinLayer = Random.Range(0, playerAnim.layerCount);  
        }
        else
        {
            chosenSkinLayer = GameManager.Instance.GetSkinID();
        }

        playerAnim.SetLayerWeight(chosenSkinLayer, 1);
        
    }

    public int GetLayerSkin()
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
            GameController.Instance.playFx(1);
            GameController.Instance.updateProgressAttack(-30);

            playerAnim.SetTrigger(animIdAttack);
            objPolled = ObjectPooling.Instance.GetPooledShoot();

            objPolled.transform.SetPositionAndRotation(posSpawn.position, posSpawn.rotation);

            objPolled.SetActive(true);
            canShot = true;

            StartCoroutine("DelayShot");
        }

    }

    public void Bomb()
    {
        if(GameController.Instance.getProgressSpecialAttackValue()>=100)
        {
            if(GetLayerSkin()==3)
            {
                GameController.Instance.playFx(6);
            }
            else
            {
                GameController.Instance.playFx(2);
            }
            
            GameController.Instance.updateProgressSpecialAttack(-100);

            playerAnim.SetTrigger(animIdBomb);
            Instantiate(GameController.Instance.specialAttackPrefab[chosenSkinLayer], posSpawn.position, posSpawn.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(isAlive)
        {
            switch(other.gameObject.tag)
            {
                case "Collectable":

                    collec = other.GetComponent<Collectable>();
                    if(collec.idCollectable.Equals("egg"))
                    {
                        GameController.Instance.updateProgressAttack(10);
                        GameController.Instance.playFx(0);

                        SpawnCollectableFeedback(other, 0);
                    }
                    else
                    {
                        GameController.Instance.updateProgressSpecialAttack(20);
                        GameController.Instance.playFx(8);

                        SpawnCollectableFeedback(other, 1);
                    }
   
                    GameController.Instance.updateScore(1);
                    other.gameObject.SetActive(false);
                    //Destroy(other.gameObject);
                    break;

                case "Enemy":
                    //isAlive = false;
                    //playerAnim.SetTrigger(animIdDeath);
                    //GameController.Instance.GameOver();
                    break;
                    
                case "WaterDamage":

                    if(!GetLayerSkin().Equals(3))
                    {
                        isAlive = false;
                        playerAnim.SetTrigger(animIdDeath);
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
        
    }

    private void SpawnCollectableFeedback(Collider2D other, int id)
    {
        GameObject collectableFeedback = Instantiate(GameController.Instance.collectableFeedbackPrefab[id]);
        
        collectableFeedback.transform.localPosition = new Vector2(other.transform.position.x, other.transform.position.y + 0.5f);
        collectableFeedback.TryGetComponent(out Rigidbody2D collectableRb);
        collectableRb.velocity = new Vector2(GameController.Instance.getSpeed(), 0);
        
        Destroy(collectableFeedback.gameObject, 0.3f);
    }

    IEnumerator DelayShot()
    {
        yield return new WaitForSeconds(delayShot);

        canShot = false;
    }

}
