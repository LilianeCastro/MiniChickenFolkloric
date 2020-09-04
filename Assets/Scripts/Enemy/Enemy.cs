using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeEnemy {  Attack, Melee, Ranged   }

public class Enemy : MonoBehaviour
{
    public Transform                posSpawnShoot;

    private Animator                enemyAnim;
    private TypeEnemy               typeEnemy;
    private int                     typeEnemyIndex;

    private void Start() {
        enemyAnim = GetComponent<Animator>();    
    }

    private void OnBecameVisible() {

        typeEnemyIndex = Random.Range(0,3);
        typeEnemy = (TypeEnemy)typeEnemyIndex;

        switch(typeEnemy)
        {
            case TypeEnemy.Attack:
                StartCoroutine("CheckDistance", new Vector2(3, 1));
                break;
            case TypeEnemy.Ranged:
                StartCoroutine("CheckDistance", new Vector2(5, 5));
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

            StopCoroutine("CheckDistance");
            StartCoroutine("ResetAnim");
        }

        StartCoroutine("CheckDistance", distanceFoAttack);
    }


    IEnumerator ResetAnim()
    {
        yield return new WaitForSeconds(1f);
        enemyAnim.SetBool("canAttack", false);
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
