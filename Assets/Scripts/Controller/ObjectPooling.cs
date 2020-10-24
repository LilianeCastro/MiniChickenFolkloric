using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoSingleton<ObjectPooling>
{
    public  GameObject[]         pooledShoot;
    private List<GameObject>     listPooledShoots;

    public  int                  pooledAmountShoot = 3;
    public  bool                 willGrow = true;

    void Start()
    {
        listPooledShoots = new List<GameObject>();

        for(int i = 0; i < pooledAmountShoot; i++)
        {
            GameObject obj = Instantiate(pooledShoot[Player.Instance.GetLayerSkin()]) as GameObject;
            obj.SetActive(false);
            listPooledShoots.Add(obj);
        }
    }

    public GameObject GetPooledShoot()
    {
        for(int i = 0; i < listPooledShoots.Count; i++)
        {
            if(!listPooledShoots[i].activeInHierarchy)
            {
                return listPooledShoots[i];
            }
        }

        if(willGrow)
        {
            GameObject obj = Instantiate(pooledShoot[Player.Instance.GetLayerSkin()]) as GameObject;
            listPooledShoots.Add(obj);
            return obj;
        }
        return null;
    }

}
