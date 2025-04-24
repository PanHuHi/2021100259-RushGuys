using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Transform respawnPoint; // ������ ����Ʈ
    public float minHeightForRespawn = -10; // �������� ���� �ּ� ����

    private void Update()
    {
        // �÷��̾��� ���� ���̸� Ȯ���Ͽ� �ּ� ���� ������ ��� ������
        if (transform.position.y <= minHeightForRespawn)
        {
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        // �÷��̾��� ��ġ�� ������ ����Ʈ�� �̵���Ŵ
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;

            // Rigidbody�� �ִٸ� �ӵ� �ʱ�ȭ
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
            // �÷��̾ ���� ���� ����Ʈ�� ������ ����Ʈ�� ����
            respawnPoint = other.transform;
            respawnRot = other.transform.rotation;
        }
    }
}
