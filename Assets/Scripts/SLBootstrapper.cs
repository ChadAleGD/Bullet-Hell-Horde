using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(ServiceLocator))]
public class SLBootstrapper : MonoBehaviour
{
    private ServiceLocator locator;
    internal ServiceLocator Locator => locator = GetComponent<ServiceLocator>();
    private bool isBootstrapped = false;



    //------------------------------------------------------------------------------------------------//


    void Awake() => Bootstrap();


    //------------------------------------------------------------------------------------------------//



    public void Bootstrap()
    {
        if (isBootstrapped) return;

        isBootstrapped = true;
        Locator.ConfigureAsGlobal();
    }
}

