using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ServiceLocator : MonoBehaviour
{

    private static ServiceLocator _global;
    public static ServiceLocator Global
    {
        get
        {
            if (_global != null) return _global;

            var container = new GameObject("[SERVICE LOCATOR]", typeof(ServiceLocator), typeof(SLBootstrapper));
            container.AddComponent<SLBootstrapper>().Bootstrap();

            return _global;
        }
    }


    private readonly ServiceManager _serviceManager = new();



    //------------------------------------------------------------------------------------------------//



    public void ConfigureAsGlobal()
    {
        if (_global == this) Debug.LogWarning("Serviced already registered as global instance.");
        else if (_global != null) Debug.LogWarning("A global Service Locator instance already exists.");
        else _global = this;
    }



    //------------------------------------------------------------------------------------------------//



    public ServiceLocator Register<T>(T service) where T : class
    {
        _serviceManager.Register(service);
        return this;
    }
    /*

        public ServiceLocator Register(Type type, object service)
        {
            _serviceManager.Register(type, service);
            return this;
        }


        public ServiceLocator GetService<T>(out T service) where T : class
        {
            if (_serviceManager.TryGet(out service)) return this;
            throw new ArgumentException($"Service of type {typeof(T)} is not registered.");
        }
    */



    //------------------------------------------------------------------------------------------------//


    private void OnDestroy() => _global = null;


    //------------------------------------------------------------------------------------------------//


#if UNITY_EDITOR
    [MenuItem("GameObject/ServiceLocator/Add SL")]
    private static void AddServiceLocator()
    {
        var container = new GameObject("[SERVICE LOCATOR]", typeof(ServiceLocator));
        container.AddComponent<SLBootstrapper>().Bootstrap();
    }
#endif


}

