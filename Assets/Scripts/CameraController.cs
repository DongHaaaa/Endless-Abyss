using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Follow")]
    [SerializeField] private Transform player;

    [Header("Camera Settings")]
    [SerializeField] private float verticalOffset = 2f;
    [SerializeField] private float smoothTime = 0.2f;
    
    private float lowestTargetY;
    private float velocityY;

    private void Start()
    {
        lowestTargetY = transform.position.y;
    }

    private void LateUpdate()
    {
        if(player == null) return;

        //Vị trí camera hướng đến
        float targetY = player.position.y + verticalOffset;

        //Camera sẽ không di lên
        lowestTargetY = Mathf.Min(lowestTargetY, targetY);

        //Di chuyển camera mượt mà nuôn, kiểu cho cam dần tiến đến mục tiêu
        float newY = Mathf.SmoothDamp(
            transform.position.y,
            lowestTargetY,
            ref velocityY,
            smoothTime
        );

        //Không cho phép camera đi lên
        newY = Mathf.Min(newY, transform.position.y);
        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );
    }
}
