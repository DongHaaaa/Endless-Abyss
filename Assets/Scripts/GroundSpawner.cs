using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float spawnRangeX = 4f;
    [SerializeField] private float maxHorizontalGap = 2.5f;
    [Header("Spike")]
    [SerializeField] private GameObject spikePrefab;
    [Range(0f, 1f)]
    [SerializeField] private float spikeChance = 0.25f;
    private float timer;
    private float lastX;
    private void Start()
    {
        lastX = transform.position.x;
        SpawnGround();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnRate)
        {
            SpawnGround();
            timer = 0f;
        }
    }
    private void SpawnGround()
    {
        float minX = Mathf.Max(transform.position.x - spawnRangeX, lastX - maxHorizontalGap);
        float maxX = Mathf.Min(transform.position.x + spawnRangeX, lastX + maxHorizontalGap);
        float spawnX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, 0f);
        GameObject ground = Instantiate(groundPrefab, spawnPosition, Quaternion.identity);
        if(spikePrefab != null && Random.value < spikeChance)
        {
            GameObject spike = Instantiate(spikePrefab, ground.transform.position + Vector3.up * 0.6f, Quaternion.identity, ground.transform);
            spike.transform.localScale = new Vector3(0.35f, 0.35f, 1f);
        }
        lastX = spawnX;
    }
}