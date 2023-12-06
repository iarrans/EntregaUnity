using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="proof", menuName ="ProofInfo")]
public class ProofInfo : ScriptableObject
{
    // Start is called before the first frame update
    public string proofName;

    public Sprite proofUISprite;

    public AudioClip soundEffect;
}
