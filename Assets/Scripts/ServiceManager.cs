using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ServiceManager
{

    private Dictionary<Type, object> _registeredServices = new();


    //------------------------------------------------------------------------------------------------//



    public void Register<T>(T service)
    {
        Type type = typeof(T);

        if (!_registeredServices.TryAdd(type, service)) Debug.LogWarning($"Service of type {type} is already registered.");
    }


    public void Register(Type type, object service)
    {
        if (!type.IsInstanceOfType(service)) throw new Exception($"Service does not match type {type}.");

        if (!_registeredServices.TryAdd(type, service)) Debug.LogWarning($"Service of type {type} is already registered.");
    }


    public T Get<T>() where T : class
    {
        Type type = typeof(T);

        if (_registeredServices.TryGetValue(type, out var service)) return service as T;

        throw new ArgumentException($"Service of type {typeof(T)} was not found.");
    }


}

