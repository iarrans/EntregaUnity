using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class trigger : MonoBehaviour
{
    [SerializeField] string TagFilter;
    [SerializeField] UnityEvent Ontriggerenter;
    [SerializeField] UnityEvent ontriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(TagFilter) && !other.gameObject.CompareTag(TagFilter)) return; 
        ontriggerExit.Invoke();
       

    }








}







