﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoSingleton<ObjectPooling>
{
    public GameObject[]     pooledObject;

    public int              pooledAmount = 5;
    public bool             willGrow = true;

    public List<GameObject> pooledObjects;

    void Start()
    {
        pooledObjects = new List<GameObject>();

        for(int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject[Player.Instance.getLayerSkin()]) as GameObject;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }


    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        if(willGrow)
        {
            GameObject obj = Instantiate(pooledObject[Player.Instance.getLayerSkin()]) as GameObject;
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }
}
