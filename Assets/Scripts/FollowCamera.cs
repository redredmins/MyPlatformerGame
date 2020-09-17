using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Vector3 cameraPos;
    [SerializeField] BoxCollider2D worldSize;

    Camera camera;
    Transform target;

    float minX;
    float maxX;
    float minY;
    float maxY;


    void Start()
    {
        camera = GetComponent<Camera>();
        target = FindObjectOfType<Player>().transform;

        float halfScreenX = camera.orthographicSize * camera.aspect;
        minX = (worldSize.offset.x - (worldSize.size.x * 0.5f)) + halfScreenX;
        maxX = worldSize.offset.x + (worldSize.size.x * 0.5f) - halfScreenX;

        float halfScreenY = camera.orthographicSize;
        minY = (worldSize.offset.y - (worldSize.size.y * 0.5f)) + halfScreenY;
        maxY = worldSize.offset.y + (worldSize.size.y * 0.5f) - halfScreenY;
    }

    void Update()
    {
        Vector3 targetPos = target.position;

        Vector3 newCameraPos = targetPos + cameraPos;

        if (newCameraPos.x < minX)
        {
            newCameraPos.x = minX;
        }
        if (newCameraPos.x > maxX)
        {
            newCameraPos.x = maxX;
        }

        if (newCameraPos.y < minY)
        {
            newCameraPos.y = minY;
        }
        if (newCameraPos.y > maxY)
        {
            newCameraPos.y = maxY;
        }

        transform.position = Vector3.Lerp(transform.position, newCameraPos, 0.05f);
    }
    
}
