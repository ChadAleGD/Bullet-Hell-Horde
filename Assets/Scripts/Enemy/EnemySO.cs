using UnityEngine;


public class EnemySO : ScriptableObject
{
    public string Name;
    public float MaxHealth;
    public float Speed;
    public float BaseDamage;
    public RuntimeAnimatorController AnimatorController;
    public EnemyBehaviorFactory BehaviorFactory;



    public class Builder
    {
        private string _name;
        private float _maxHealth;
        private float _baseDamage;
        private float _speed;
        private RuntimeAnimatorController _animatorController;
        private EnemyBehaviorFactory _behaviorFactory;


        public Builder WithName(string name)
        {
            _name = name;

            return this;
        }

        public Builder WithMaxHealth(float maxHealth)
        {
            _maxHealth = maxHealth;

            return this;
        }

        public Builder WithBaseDamage(float baseDamage)
        {
            _baseDamage = baseDamage;

            return this;
        }

        public Builder WithSpeed(float speed)
        {
            _speed = speed;

            return this;
        }

        public Builder WithAnimatorController(RuntimeAnimatorController animator)
        {
            _animatorController = animator;

            return this;
        }

        public Builder WithBehavior(EnemyBehaviorFactory behaviorFactory)
        {
            _behaviorFactory = behaviorFactory;

            return this;
        }

        public EnemySO Build()
        {
            var enemy = CreateInstance<EnemySO>();
            enemy.Name = _name;
            enemy.MaxHealth = _maxHealth;
            enemy.Speed = _speed;
            enemy.BaseDamage = _baseDamage;
            enemy.AnimatorController = _animatorController;
            enemy.BehaviorFactory = _behaviorFactory;

            return enemy;
        }

    }

}

