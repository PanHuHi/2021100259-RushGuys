using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_intrig_tf : MonoBehaviour
{
    public GameObject water;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            if (water.GetComponent<Collider>().isTrigger)
            {
                water.GetComponent<Collider>().isTrigger = false;
            }
            else water.GetComponent<Collider>().isTrigger = true;
        }
    }
}
