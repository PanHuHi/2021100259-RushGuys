using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Item : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Y_move());
    }

    IEnumerator Y_move()
    { 
        for(int i = 0; i< 200; i++)
        {
            transform.position += new Vector3(0, 0.01f,0);
            yield return new WaitForSecondsRealtime(0.05f);
        }
        for (int i = 0; i < 200; i++)
        {
            transform.position -= new Vector3(0, 0.01f, 0);
            yield return new WaitForSecondsRealtime(0.05f);
        }
        StartCoroutine(Y_move());
    }
}
