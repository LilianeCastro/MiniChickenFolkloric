using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    void OnJump()
    {
        print("Pulo");
        Player.Instance.Jump();
    }

    void OnShot()
    {
        print("Tiro");
        Player.Instance.Fire();
    }

    void OnBomb()
    {
        print("Bomba");
        Player.Instance.Bomb();
    }
}
