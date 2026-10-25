using UnityEngine;

public class GroundMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float destroyY = 7f;

    private void Update()
    {
        transform.position +=
            Vector3.up * moveSpeed * Time.deltaTime;

        if (transform.position.y > destroyY)
        {
            Destroy(gameObject);
        }
    }
}