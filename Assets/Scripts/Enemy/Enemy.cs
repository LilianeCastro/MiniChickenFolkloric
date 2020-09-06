using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeEnemy {  Melee, Attack, Ranged   }
public enum NameEnemy {  BichoPapao, Boiuna, Lobisomem   }


public class Enemy : MonoBehaviour
{
    public Transform                posSpawnShoot;
    public NameEnemy                nameEnemy;
    public int                      speedShot;

    private BoxCollider2D           boxCollider;  
    private Animator                enemyAnim;
    private GameObject              attackEnemyTemp;
    private TypeEnemy               typeEnemy;
    private int                     typeEnemyIndex;
    private bool                    isAttackRanged;

    private void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
        enemyAnim = GetComponent<Animator>();    
    }

    private void OnBecameVisible() {

        typeEnemyIndex = 2;//Random.Range(0,3);
        typeEnemy = (TypeEnemy)typeEnemyIndex;

        switch(typeEnemy)
        {
            case TypeEnemy.Attack:
                StartCoroutine("CheckDistance", new Vector2(2.5f, 0.5f));
                break;
            case TypeEnemy.Ranged:
                StartCoroutine("CheckDistance", new Vector2(8f, 5f));
                break;
            case TypeEnemy.Melee:
                StartCoroutine("CheckDistance", new Vector2(10f, 10f));
                break;      
        }
        
    }

    IEnumerator CheckDistance(Vector2 distanceFoAttack)
    {
        yield return new WaitForEndOfFrame();

        if(DistanceX() < distanceFoAttack.x && DistanceY() < distanceFoAttack.y)
        {
            enemyAnim.SetInteger("idAnimation", (int)typeEnemy);
            enemyAnim.SetBool("canAttack", true);

            if(typeEnemy.Equals(TypeEnemy.Ranged) && !isAttackRanged)
            {
                isAttackRanged = true;
                StartCoroutine("SpawnAttack");
            }

            StopCoroutine("CheckDistance");
            StartCoroutine("ResetAnim");

            StartCoroutine("TrasnformColliderToTrigger");
        }

        StartCoroutine("CheckDistance", distanceFoAttack);
    }


    IEnumerator ResetAnim()
    {
        yield return new WaitForSeconds(1f);
        enemyAnim.SetBool("canAttack", false);
    }

    IEnumerator TrasnformColliderToTrigger()
    {
        yield return new WaitForEndOfFrame();
        if(Vector2.Distance(this.transform.position, Player.Instance.transform.position) < 2f)
        {
            boxCollider.isTrigger = true;
            StopCoroutine("TrasnformColliderToTrigger");
        }
        StartCoroutine("TrasnformColliderToTrigger");
    }

    IEnumerator SpawnAttack()
    {
        yield return new WaitForSeconds(1f);
        attackEnemyTemp = Instantiate(GameController.Instance.attackEnemyRanged[(int)nameEnemy], posSpawnShoot.position, posSpawnShoot.rotation);
        attackEnemyTemp.TryGetComponent(out Rigidbody2D attackRb);
        attackRb.velocity = Vector2.left * speedShot;
    }

     private float DistanceX()
    {
        return transform.position.x - Player.Instance.transform.position.x;
    }

    private float DistanceY()
    {
        return transform.position.y - Player.Instance.transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.tag=="Shot")
        {
            Destroy(this.gameObject);
        }

    }

}
