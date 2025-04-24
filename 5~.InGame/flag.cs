using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flag : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (End.instance.PointNum == 2)
            {
                End.instance.GameFinish();
                Debug.Log("결승점 통과");
            }
        }
    }
}
