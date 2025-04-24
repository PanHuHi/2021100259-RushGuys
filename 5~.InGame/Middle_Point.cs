using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Middle_Point : MonoBehaviour

{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            End.instance.PointNum = 1;
        }
    }
}
