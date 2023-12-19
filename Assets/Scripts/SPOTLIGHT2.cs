using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundObject : MonoBehaviour
{
    public Transform targetObject;
    public float rotationSpeed = 5f;

    void Update()
    {
        // Rotate the empty GameObject around the target object
        transform.RotateAround(targetObject.position, Vector3.up, rotationSpeed * Time.deltaTime);

        // Always aim the spotlight at the target object
        transform.LookAt(targetObject.position);
    }
}
