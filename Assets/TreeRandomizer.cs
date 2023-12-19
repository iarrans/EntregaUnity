using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRandomizer : MonoBehaviour
{
    // Define the range of heights and widths for your trees
    public float minHeight = 1.0f;
    public float maxHeight = 5.0f;
    public float minWidth = 0.5f;
    public float maxWidth = 2.0f;

    void Start()
    {
        // Randomly set the height and width of the tree
        float randomHeight = Random.Range(minHeight, maxHeight);
        float randomWidth = Random.Range(minWidth, maxWidth);

        // Apply the random scale to the tree prefab
        transform.localScale = new Vector3(randomWidth, randomHeight, randomWidth);
    }
}
