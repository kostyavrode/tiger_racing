using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocatorInstaller : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] services;
    private void Awake()
    {
        foreach (var service in services)
        {
            ServiceLocator.AddService(service);
            Debug.Log("Added Service: " + service);
        }
    }
    private void OnDisable()
    {
        foreach(var service in services)
        {
            ServiceLocator.RemoveService(service);
        }
    }
}
