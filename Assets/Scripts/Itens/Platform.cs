using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform            posSpawnCollectable;
    private int                 idPool;

    public void InstantiateCollectable()
    {
        if(GameController.Instance.CanSpawnAbovePercent(15))
        {
            int idChosen = Random.Range(0, GameController.Instance.collectablePlusPrefab.Length);
            GameController.Instance.instantiateObjects(posSpawnCollectable, GameController.Instance.collectablePlusPrefab, GameController.Instance.collectablePlusPrefab.Length, 2, "");
            
        }
        else if(GameController.Instance.CanSpawnAbovePercent(50))
        {
            int idChosen = Random.Range(0, GameController.Instance.collectablePrefab.Length);
            GameController.Instance.instantiateObjects(posSpawnCollectable, GameController.Instance.collectablePrefab, GameController.Instance.collectablePrefab.Length, 2, "");
        }  
    }

    public void InstantiateCollectableWithEnemy()
    {
        int idChosen = Random.Range(0, GameController.Instance.collectablePlusPrefab.Length);
        GameController.Instance.instantiateObjects(posSpawnCollectable, GameController.Instance.collectablePlusPrefab, GameController.Instance.collectablePlusPrefab.Length, 2, "");
    }

    public void SetInfo(int idPoolInfo)
    {
        this.idPool = idPoolInfo;
        
    }

    private void OnBecameInvisible() {
        //transform.parent = null;
        //ObjectPoolingManager.Instance.CoolObject(this.gameObject, idPool);
    }
}
