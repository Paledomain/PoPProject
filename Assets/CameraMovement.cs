using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    Camera _cameraComponent;
    Vector2 bottomLeft;
    Vector2 topRight;

    // Start is called before the first frame update
    void Start()
    {
        _cameraComponent = GetComponent<Camera>();
        UpdateViewportLimits();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > topRight.x + 0.1f)
        {
            transform.position += new Vector3(_cameraComponent.orthographicSize * _cameraComponent.aspect * 2, 0.0f, 0.0f);
            UpdateViewportLimits();
        }
        else if (player.transform.position.x < bottomLeft.x - 0.1f)
        {
            transform.position -= new Vector3(_cameraComponent.orthographicSize * _cameraComponent.aspect * 2, 0.0f, 0.0f);
            UpdateViewportLimits();
        }
        if (player.transform.position.y > topRight.y + 0.1f)
        {
            transform.position += new Vector3(0.0f, _cameraComponent.orthographicSize * 2, 0.0f);
            UpdateViewportLimits();
        }
        else if (player.transform.position.y < bottomLeft.y - 0.1f)
        {
            transform.position -= new Vector3(0.0f, _cameraComponent.orthographicSize * 2, 0.0f);
            UpdateViewportLimits();
        }
    }

    void UpdateViewportLimits()
    {
        Vector2 diagonal = new Vector2(_cameraComponent.orthographicSize * _cameraComponent.aspect, _cameraComponent.orthographicSize);
        bottomLeft = (Vector2)transform.position - diagonal;
        topRight = (Vector2)transform.position + diagonal;
    }
}
