using UnityEngine;



public interface IDamageable
{
    void TakeDamage(Damage damage);

}


public struct Damage
{
    public float Potency;
    public float PushForce;
}