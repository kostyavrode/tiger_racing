using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public static class ServiceLocator 
{
    private static readonly List<object> services= new List<object>();
    public static T GetService<T>()
    {
        foreach(var service in services)
        {
            if (service is T result) 
                return result;
        }
        throw new Exception($"Service of type {typeof (T).FullName} is not found");
    }
    public static void AddService(object service)
    {
        services.Add(service);
    }
    public static void RemoveService(object service)
    {  services.Remove(service); }
}
