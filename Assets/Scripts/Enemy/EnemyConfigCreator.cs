using UnityEngine;




public class EnemyConfigCreator : MonoBehaviour
{
    public string EnemyName;
    public float MaxHealth;
    public float Speed;
    public float BaseDamage;
    public RuntimeAnimatorController AnimationController;
    public EnemyBehaviorFactory BehaviorFactory;


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
        if (BehaviorFactory == null)
        {
            message = "Behavior has not been set.";
            return false;
        }

        message = "";
        return true;

    }




    private void Awake() => Destroy(gameObject);
}