using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    void OnJump()
    {
        Player.Instance.Jump();
    }

    void OnShot()
    {
        Player.Instance.Fire();
    }

    void OnBomb()
    {
        Player.Instance.Bomb();
    }
}
