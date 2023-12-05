using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float horizotal = 0.0f;
    private float vertical = 0.0f;

    private void Update()
    {
        vertical += speedH * Input.GetAxis("Mouse X");
        horizotal -= speedH * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(horizotal, vertical, 0);
    }
}
