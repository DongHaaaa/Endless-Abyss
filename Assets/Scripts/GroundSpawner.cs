using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab;
    public float spawnRate = 2f;
    private float timer = 0;
    public float PositionX = 10;
    void Start()
    {
        spawnGround();
    }
    void Update()
    {
        if(timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnGround();
            timer = 0;
        }
        
    }
    void spawnGround()
    {
        float lowestX = transform.position.x - PositionX;
        float highestX = transform.position.x + PositionX;
        Instantiate(groundPrefab, new Vector3(transform.position.x, Random.Range(lowestX, highestX), 0), transform.rotation);
    }
}
