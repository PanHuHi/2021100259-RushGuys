using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Net;

public class FreeLook_Cam : MonoBehaviour
{
    CinemachineFreeLook freeLookCamera;

    GameObject Player;
    GameObject playerCharacter_Clone;
    void Start()
    {
       // StartCoroutine(dsfsdf());
    }
    IEnumerator dsfsdf()
    {
        yield return new WaitForSecondsRealtime(3);
        freeLookCamera = this.GetComponent<CinemachineFreeLook>();
        playerCharacter_Clone = GameObject.Find("PlayerCharacter(Clone)");
        for (int i = 0; i < playerCharacter_Clone.transform.childCount; i++)
        {
            if (playerCharacter_Clone.transform.GetChild(i).gameObject.activeSelf)
            {
                Player = playerCharacter_Clone.transform.GetChild(i).gameObject;
            }
        }
        Debug.Log(Player);
        freeLookCamera.Follow = Player.transform;
        freeLookCamera.LookAt = Player.transform;

        

    }
}
