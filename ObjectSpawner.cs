using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject CherryPrefab; // ť�� ������
    public float spawnInterval = 1.0f; // ť�� ���� ����
    public Vector3 spawnArea = new Vector3(10, 10, 10); // ���� ���� ũ��
    public float destroyTime = 3.0f; // ������ ť�갡 ������������ �ð�

    void Start()
    {
        StartCoroutine(SpawnCubes());
    }

    IEnumerator SpawnCubes()
    {
        while (true)
        {
            SpawnCherry();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCherry()
    {
        // ���� ��ġ ����
        float x = Random.Range(transform.position.x-2, transform.position.x+ 2);
        float y = spawnArea.y; // �ϴÿ��� ���������� y ��ġ ����
        float z = Random.Range(transform.position.z - 4, transform.position.z + 4);
        Vector3 spawnPosition = new Vector3(x, y, z);

        // ť�� ����
        GameObject Cherry = Instantiate(CherryPrefab, spawnPosition, Quaternion.identity);

        // 3�� �� ť�� ����
        Destroy(Cherry, destroyTime);
    }
}
