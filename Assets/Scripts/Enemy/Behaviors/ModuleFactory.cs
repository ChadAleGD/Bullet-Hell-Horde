using UnityEngine;




public abstract class ModuleFactory : ScriptableObject
{
    public abstract IModule AttachModule(GameObject host);
}