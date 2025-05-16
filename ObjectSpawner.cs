using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject CherryPrefab; // 큐브 프리팹
    public float spawnInterval = 1.0f; // 큐브 생성 간격
    public Vector3 spawnArea = new Vector3(10, 10, 10); // 생성 영역 크기
    public float destroyTime = 3.0f; // 생성된 큐브가 사라지기까지의 시간

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
        // 랜덤 위치 생성
        float x = Random.Range(transform.position.x-2, transform.position.x+ 2);
        float y = spawnArea.y; // 하늘에서 떨어지도록 y 위치 고정
        float z = Random.Range(transform.position.z - 4, transform.position.z + 4);
        Vector3 spawnPosition = new Vector3(x, y, z);

        // 큐브 생성
        GameObject Cherry = Instantiate(CherryPrefab, spawnPosition, Quaternion.identity);

        // 3초 후 큐브 삭제
        Destroy(Cherry, destroyTime);
    }
}
