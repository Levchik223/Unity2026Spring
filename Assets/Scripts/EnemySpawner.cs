using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int Amount = 20;
    public GameObject EnemyPrefab;
    public float Offset = 1f;

    void CreateEnemies()
    {
        Vector3 StartPosition = transform.position;
        for (int i = 0; i < Amount; i++)
        {
            CreateEnemy(StartPosition, i);
        }
    }

    private void CreateEnemy(Vector3 StartPosition, int i)
    {
        Vector3 SpawnPosition = new Vector3(
            StartPosition.x + Offset * i,
            StartPosition.y,
            StartPosition.z);

        Instantiate(EnemyPrefab, SpawnPosition, Quaternion.identity);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
