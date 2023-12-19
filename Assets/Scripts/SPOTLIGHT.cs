using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSpotlight : MonoBehaviour
{
    public float rotationSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the spotlight around its up axis (Y-axis)
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
