
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
        if(isTriggered) return;

        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            StartCoroutine(StartFalling());
        }
    }

    private IEnumerator StartFalling()
    {
        isTriggered = true;
        // Cho truoc khi nen roi
        yield return new WaitForSeconds(fallDelay);
        // Bat mo gravity va cho phep nen roi
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = fallGravity;

    }
}
