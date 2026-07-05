using UnityEngine;
using System.Collections;

public class PointSpawner : MonoBehaviour
{
    public GameObject pointPrefab;
    public float spawnInterval = 1.5f;

    // Beyaz duvarlarýnýn sýnýr koordinatlarý
    public Vector2 minBounds;
    public Vector2 maxBounds;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (Object.FindAnyObjectByType<playercontroller>() != null)
        {
            SpawnPoint();

            yield return new WaitForSeconds(spawnInterval);
        }

    }

    void SpawnPoint()
    {
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        Instantiate(pointPrefab, spawnPosition, Quaternion.identity);
    }
}