using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    public GameObject[] Obj;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(destroyObject());
        }
    }
    IEnumerator destroyObject()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        if (Obj != null)
        {
            foreach (GameObject obj in Obj)
            {
                obj.SetActive(false);
            }
        }
    }

}
