using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Wave {
    public string waveName;
    public int enemyCount;
    public float spawnInterval;
    public GameObject[] enemies;
}
public class EnemySpawningScript : MonoBehaviour
{   [SerializeField] private Wave[] waves; 
    [SerializeField] private Tilemap spawnableTilemap;
    [SerializeField] private float spawnDistance = 2f;
    [SerializeField] private bool canSpawn = true;

    private int currentWaveIndex = 0;
    private int enemiesRemainingToSpawn;
    public int enemiesCurrentlyAlive; // Changed to public so enemies can reach it

    private void Start() {
        if (waves.Length > 0) {
            StartCoroutine(SpawnWaveRoutine());
        }
    }

    private System.Collections.IEnumerator SpawnWaveRoutine() {
        while (currentWaveIndex < waves.Length && canSpawn) {
            Wave currentWave = waves[currentWaveIndex];
            enemiesRemainingToSpawn = currentWave.enemyCount;

            Debug.Log("Starting Wave: " + currentWave.waveName);

            while (enemiesRemainingToSpawn > 0) {
                TrySpawnOneEnemy(currentWave);
                enemiesRemainingToSpawn--;
                yield return new WaitForSeconds(currentWave.spawnInterval);
            }

            // Wait until the scene is clear of enemies
            while (enemiesCurrentlyAlive > 0) {
                yield return new WaitForSeconds(1f);
            }

            Debug.Log("Wave Complete!");
            currentWaveIndex++;
            yield return new WaitForSeconds(5f); // Rest time between waves
        }
    }

    private void TrySpawnOneEnemy(Wave wave) {
        bool spawned = false;
        int attempts = 0;
        int maxAttempts = 20;

        while (!spawned && attempts < maxAttempts) {
            Vector3 spawnPos = GetRandomPositionOutsideCamera();
            Vector3Int cellPos = spawnableTilemap.WorldToCell(spawnPos);

            if (spawnableTilemap.HasTile(cellPos)) {
                int randomIndex = Random.Range(0, wave.enemies.Length);
                GameObject enemy = Instantiate(wave.enemies[randomIndex], spawnPos, Quaternion.identity);
                
                // Track life (Assuming your enemy has a way to tell us it died)
                enemiesCurrentlyAlive++; 
                spawned = true;
            } else {
                attempts++;
            }
        }
    }

    private Vector3 GetRandomPositionOutsideCamera() {
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
     /*
    [SerializeField] private Wave[] waves; 
    [SerializeField] private Tilemap spawnableTilemap;
    [SerializeField] private float spawnDistance = 2f;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private bool canSpawn = true;


    private int currentWaveIndex = 0;
    private int enemiesRemainingToSpawn;
    private int enemiesCurrentlyAlive;
    private void Start() {
        StartCoroutine(SpawnWave());
    }
    private System.Collections.IEnumerator SpawnWave() {
        Wave currentWave = waves[currentWaveIndex];
        enemiesRemainingToSpawn = currentWave.enemyCount;

        while (enemiesRemainingToSpawn > 0) {
            SpawnEnemy(currentWave);
            enemiesRemainingToSpawn--;
            yield return new WaitForSeconds(currentWave.spawnInterval);
        }

        // wait till all dead
        while (enemiesCurrentlyAlive > 0) {
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Wave Complete!");
        currentWaveIndex++;
        
        if (currentWaveIndex < waves.Length) {
            yield return new WaitForSeconds(5f);
            StartCoroutine(SpawnWave());
        }
    }
    private System.Collections.IEnumerator SpawnEnemy(Wave wave) {
    {
        while (canSpawn)
    {
        yield return new WaitForSeconds(spawnRate);

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
    }*/



    /*
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
    }*/
    

}
