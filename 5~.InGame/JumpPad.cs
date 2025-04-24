using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.gameObject);
            Rigidbody PlayerRB = other.gameObject.GetComponent<Rigidbody>();
            PlayerRB.velocity = new Vector3(0, 10, 0);
        }
    }
}
