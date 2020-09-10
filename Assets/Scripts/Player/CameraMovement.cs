using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float sensitivity = 1.0f;

    public float speedH = 2.0f;
    public float speedV = 2.0f;
    private float yaw = 0.0f;
    void FixedUpdate()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        player.transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);

    }
}
