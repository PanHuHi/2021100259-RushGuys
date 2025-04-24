using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Point : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (End.instance.PointNum != 2)
            {
                End.instance.PointNum = 0;
            }
        }
    }
}
