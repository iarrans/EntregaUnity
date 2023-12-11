using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class trigger : MonoBehaviour
{
    [SerializeField] string TagFilter;
    [SerializeField] UnityEvent ontriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;

    void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(TagFilter) && !other.gameObject.CompareTag(TagFilter)) return; 
        ontriggerEnter.Invoke();
       

    }

     void OnTriggerExit(Collider other)
    {
        if (!string.IsNullOrEmpty(TagFilter) && !other.gameObject.CompareTag(TagFilter)) return;
        onTriggerExit.Invoke();


    }






}







