using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class End_Point : MonoBehaviour
{
    public Collider C_endColl;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (End.instance.PointNum == 1)
            {
                End.instance.PointNum = 2;
                C_endColl.isTrigger = true;
            }
        }
    }
}
