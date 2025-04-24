using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objToRetation : MonoBehaviour
{
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Pushing(collision));
        }
    }
    IEnumerator Pushing(Collision collision)
    {
        yield return new WaitForSeconds(0.05f);
        collision.transform.position += (Vector3.right/10);
        yield return null;
    }
}
