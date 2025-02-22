using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTurn : MonoBehaviour
{
    public Transform player; // Player's transform
    public Vector3 offset = new Vector3(0, 2, -4); // Offset behind player
    public float sensitivity = 2.0f;

    private Vector2 turn;
    private float minY = -30f; // Min angle to prevent camera from flipping
    private float maxY = 60f; // Max angle to prevent looking too high

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        TurnScreen();
    }

    void TurnScreen()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;

        turn.y = Mathf.Clamp(turn.y, minY, maxY); // Clamp vertical angle

        // Rotate camera around player
        Quaternion rotation = Quaternion.Euler(-turn.y, turn.x, 0);
        transform.position = player.position + rotation * offset;
        transform.LookAt(player.position);
    }
}
