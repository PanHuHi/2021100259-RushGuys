using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class End : MonoBehaviour
{
    public static End instance;
    public int PointNum;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PointNum = 0;
    }

    public void GameFinish()
    {
        //게임 종료
        GameManager.Instance.GameFinish();
    }
}
