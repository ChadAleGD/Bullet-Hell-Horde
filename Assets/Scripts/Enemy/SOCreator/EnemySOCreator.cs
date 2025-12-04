using System.Collections.Generic;
using UnityEngine;




public class EnemySOCreator : MonoBehaviour
{
    public string EnemyName;
    public float MaxHealth;
    public float Speed;
    public float BaseDamage;
    public RuntimeAnimatorController AnimationController;
    public List<ModuleFactory> ModuleFactories;


    // Validation method to ensure that the editor has each
    // field to make a fully populated scriptable object
    public bool Validate(out string message)
    {
        if (string.IsNullOrEmpty(EnemyName))
        {
            message = "Name of enemy cannot be empty.";
            return false;
        }
        if (MaxHealth <= 0)
        {
            message = "Max health must be greater than 0";
            return false;
        }
        if (Speed <= 0)
        {
            message = "Speed must be greater than 0";
            return false;
        }
        if (BaseDamage <= 0)
        {
            message = "Base damage must be greater than 0";
            return false;
        }
        if (AnimationController == null)
        {
            message = "Animation controller has not been set.";
            return false;
        }
        if (ModuleFactories == null || ModuleFactories.Count <= 0)
        {
            message = "Modules have not been set.";
            return false;
        }


        message = "";
        return true;

    }


    // This MonoBehaviour has no purpose during runtime
    // Deleting the GameObject cleans up scene hierarchy
    private void Awake() => Destroy(gameObject);
}