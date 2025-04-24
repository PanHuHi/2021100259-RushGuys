using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Transform respawnPoint; // 리스폰 포인트
    public float minHeightForRespawn = -10; // 리스폰을 위한 최소 높이

    private void Update()
    {
        // 플레이어의 현재 높이를 확인하여 최소 높이 이하일 경우 리스폰
        if (transform.position.y <= minHeightForRespawn)
        {
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        // 플레이어의 위치를 리스폰 포인트로 이동시킴
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;

            // Rigidbody가 있다면 속도 초기화
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                transform.rotation = respawnRot;
            }
        }
    }
    Quaternion respawnRot;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            // 플레이어가 밟은 스폰 포인트를 리스폰 포인트로 설정
            respawnPoint = other.transform;
            respawnRot = other.transform.rotation;
        }
    }
}
