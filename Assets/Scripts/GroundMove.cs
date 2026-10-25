using UnityEngine;

public class GroundMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    void Start()
    {
        
    }
    void Update()
    {
        transform.position = transform.position + (Vector3.up * moveSpeed) * Time.deltaTime;
    }
}
