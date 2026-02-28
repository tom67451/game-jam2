using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawningScript : MonoBehaviour
{
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private bool canSpawn = true;
    [SerializeField] private float spawnDistance = 2f;
    [SerializeField] private Tilemap spawnableTilemap;

    private void Start()
    {
        StartCoroutine(Spawner());

       
    }
    private System.Collections.IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
    
    while (canSpawn)
    {
        yield return wait;

        bool spawned = false;
        int attempts = 0;
        int maxAttempts = 20;

        while (!spawned && attempts < maxAttempts)
        {
            Vector3 spawnPos = GetRandomPositionOutsideCamera();
            Vector3Int cellPos = spawnableTilemap.WorldToCell(spawnPos);

            if (spawnableTilemap.HasTile(cellPos))
            {
                int randomIndex = Random.Range(0, enemyPrefabs.Length);
                Instantiate(enemyPrefabs[randomIndex], spawnPos, Quaternion.identity);
                spawned = true;
            }
            else
            {
                attempts++;
            }
        }

        if (!spawned)
        {
            Debug.LogWarning("Spawner: Could not find a valid tile after " + maxAttempts + " tries.");
        }
    }
    }
    private Vector3 GetRandomPositionOutsideCamera()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        Vector2 spawnDir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        return new Vector3(
            cam.transform.position.x + spawnDir.x * (width / 2 + spawnDistance),
            cam.transform.position.y + spawnDir.y * (height / 2 + spawnDistance),
            0f
        );
    }
    

}
