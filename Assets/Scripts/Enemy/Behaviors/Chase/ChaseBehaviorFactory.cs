using UnityEngine;


[CreateAssetMenu(fileName = "ChaseBehaviorModule", menuName = "Modules/Enemy Behavior/Chase")]
public class ChaseBehaviorFactory : ModuleFactory
{
    public override IModule AttachModule(GameObject host) => host.AddComponent<ChaseBehavior>();
}