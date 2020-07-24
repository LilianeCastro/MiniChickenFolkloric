using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform posSpawnCollectable;

    void Start()
    {
        GameController.Instance.InstantiateObjects(posSpawnCollectable, GameController.Instance.platformPrefab);
    }
}
