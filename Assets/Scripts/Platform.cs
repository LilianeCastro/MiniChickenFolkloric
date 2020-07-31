using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform posSpawnCollectable;

    void Start()
    {
        int idChosen = Random.Range(0, GameController.Instance.collectablePrefab.Length);
        GameController.Instance.instantiateObjects(posSpawnCollectable, GameController.Instance.collectablePrefab, GameController.Instance.collectablePrefab.Length, 2);
    }
}
