
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 0.6f;
    [SerializeField] private float fallGravity = 1.5f;

    private Rigidbody2D rb;
    private bool isTriggered = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isTriggered) return;

        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            StartCoroutine(StartFalling());
        }
    }

    private IEnumerator StartFalling()
    {
        isTriggered = true;

        // Chờ trước khi nền bắt đầu rơi
        yield return new WaitForSeconds(fallDelay);

        // Bật mô phỏng trọng lực
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = fallGravity;

        // Dọn nền sau 3 giây
        Destroy(gameObject, 3f);
    }
}
