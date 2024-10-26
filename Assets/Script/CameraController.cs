using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public Tilemap tilemap; // Reference to your tilemap

    private float minX, maxX, minY, maxY;

    void Start()
    {
        /*
        // Get tilemap bounds in world space
        Bounds tilemapBounds = tilemap.localBounds;

        // Get camera size
        float camHeight = Camera.main.orthographicSize * 2f;
        float camWidth = camHeight * Camera.main.aspect;

        // Calculate camera boundaries based on tilemap bounds and camera size
        minX = tilemapBounds.min.x + camWidth / 2;
        maxX = tilemapBounds.max.x - camWidth / 2;
        minY = tilemapBounds.min.y + camHeight / 2;
        maxY = tilemapBounds.max.y - camHeight / 2;
        */
    }

    void LateUpdate()
    {
        /*
        Vector3 desiredPosition = player.position + offset;

        // Clamp the camera's position within the calculated boundary
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

        Vector3 clampedPosition = new Vector3(clampedX, clampedY, -10);

        // Smoothly move the camera to the target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
        transform.position = smoothedPosition;
        */
        Vector3 position = new Vector3(player.position.x, player.position.y, -10);

        transform.position = position;
    }
}
