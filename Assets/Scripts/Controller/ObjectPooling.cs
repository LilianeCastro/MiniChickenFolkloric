using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoSingleton<ObjectPooling>
{
    public  GameObject[]         pooledShoot;
    private List<GameObject>     listPooledShoots;
    
    //public  GameObject           pooledGround;
    //private List<GameObject>[]   listPooledGrounds;
    //private List<GameObject>     listPooledGrounds;


    public  int                  pooledAmountShoot = 3;
    public  int                  pooledAmountGround = 6;
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

        /*listPooledGrounds = new List<GameObject>();

        for(int i = 0; i < pooledAmountGround; i++)
        {
            GameObject obj = Instantiate(pooledGround) as GameObject;
            obj.SetActive(false);
            listPooledGrounds.Add(obj);
        }*/

        /*listPooledGrounds = new List<GameObject>[pooledGround.Length];

        for(int i = 0; i < pooledGround.Length; i++)
        {
            listPooledGrounds[i] = new List<GameObject>();
        }*/
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

    /*public GameObject GetPooledGround()
    {
        print(listPooledGrounds.Count);
        for(int i = 0; i < listPooledGrounds.Count; i++)
        {
            print(listPooledGrounds[i]);
            if(!listPooledGrounds[i].activeInHierarchy)
            {
                return listPooledGrounds[i];
            }
        }

        if(willGrow)
        {
            GameObject obj = Instantiate(pooledGround) as GameObject;
            listPooledGrounds.Add(obj);
            return obj;
        }

        return null;
    }*/

    /*public GameObject GetPooledGround()
    {
        int randomChoose = Random.Range(0, listPooledGrounds.Length);

        print(randomChoose);
        for(int i = 0; i < listPooledGrounds[randomChoose].Count; i++)
        {
            GameObject obj = listPooledGrounds[randomChoose][i];
            print(listPooledGrounds[randomChoose][i]);

            if(obj == null)
            {
                obj = Instantiate(pooledGround[randomChoose]) as GameObject;
                obj.SetActive(false);
                listPooledGrounds[randomChoose][i] = obj;
                return obj;
            }

            if(!obj.activeInHierarchy)
            {
                print("xxx");
                return obj;
            }
        }

        if(willGrow)
        {
            print("Grouw");
            GameObject obj = Instantiate(pooledGround[randomChoose]) as GameObject;
            listPooledGrounds[randomChoose].Add(obj);
            return obj;
        }
        return null;
    }*/

}
