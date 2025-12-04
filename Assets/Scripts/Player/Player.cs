using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Gameplay.Player
{

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(AudioSource))]
    public class Player : MonoBehaviour, IDamageable
    {

        private PlayerInputActions _inputActions;
        private Rigidbody2D _rigidBody;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private AudioSource _audioSource;


        [Header("Movement")]
        [SerializeField] private float _moveSpeed = .7f;
        private Vector2 _movementDelta;
        private bool _isMoving = false;
        private bool _isAttacking = false;


        [Header("Combat")]
        [SerializeField] private float _health = 50;
        public float Health => _health;
        private bool _isAlive = true;

        [SerializeField] private float _damage = 1;
        public float Damage => _damage;

        private readonly float _invincibilityPeriod = .4f;
        private bool _currentlyInvincible = false;
        private WaitForSeconds _invincibilityTick;

        private const float BaseKnockback = .1f;
        [SerializeField] private float _knockBack = .1f;
        private float _cooldown = 2f;
        private WaitForSeconds _cooldownWait = new(2f);


        [SerializeField, Min(0)] private float _attackDistance;
        public float AttackDistance => _attackDistance;
        [SerializeField, Range(0, 360), Min(80)] private float _attackRadius;
        public float AttackRadius => _attackRadius;

        [Range(0, 360)] public float AttackPivotPoint = 0;


        private Vector2 _lastMovementDirection = Vector2.down;
        [SerializeField] private LayerMask _enemyLayer;


        private IInteractable _interactable = null;


        [Header("Trigger Colliders")]
        [SerializeField] private CustomTrigger _interactionTrigger;




        //---------------------------------------------------------------------------------------------------//




        private void Awake()
        {
            //_serviceLocator = ServiceLocator.Global.Register(typeof(Player), this);
            _inputActions = new();

            _animator = GetComponent<Animator>();
            _rigidBody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();


        }



        private void Start()
        {
            _inputActions.Enable();

            _inputActions.Player.Movement.performed += var => _movementDelta = var.ReadValue<Vector2>();
            _inputActions.Player.Movement.canceled += var => _movementDelta = Vector2.zero;
            _inputActions.Player.Interact.performed += var => Interact();
            StartCoroutine(AttackLoop());

            _invincibilityTick = new(_invincibilityPeriod / 8f);
        }


        private void OnEnable()
        {
            EventBus.Subscribe(EventType.OnUpgradePanelOpen, DisableInput);
            EventBus.Subscribe(EventType.OnUpgradePanelClose, EnableInput);

            _interactionTrigger.OnTriggerEntered += EnteredInteractRange;
            _interactionTrigger.OnTriggerExited += ExitedInteractRange;
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(EventType.OnUpgradePanelOpen, DisableInput);
            EventBus.Unsubscribe(EventType.OnUpgradePanelClose, EnableInput);

            _interactionTrigger.OnTriggerEntered -= EnteredInteractRange;
            _interactionTrigger.OnTriggerExited -= ExitedInteractRange;
        }



        private void FixedUpdate()
        {
            SetPivot(_movementDelta);
            // Movement delta through input system has been changed to not be normalized by default to set the pivot easier
            Move(_movementDelta.normalized);
            Animate();
        }



        //---------------------------------------------------------------------------------------------------//



        public void Move(Vector2 delta) => _rigidBody.linearVelocity = new Vector3(delta.x * _moveSpeed, delta.y * _moveSpeed);

        private void DisableInput() => _inputActions.Disable();
        private void EnableInput() => _inputActions.Enable();


        private void SetPivot(Vector2 unNormalizedDelta)
        {

            if (unNormalizedDelta == Vector2.zero) return;

            _lastMovementDirection = unNormalizedDelta;

            // Temporary and ugly, but works for short term to set pivot of direction the player is facing for attack
            //TODO change this to a dictionary for lookup
            switch ((unNormalizedDelta.x, unNormalizedDelta.y))
            {
                case (1, 0):
                    AttackPivotPoint = 0;
                    break;
                case (1, 1):
                    AttackPivotPoint = 45;
                    break;
                case (0, 1):
                    AttackPivotPoint = 90;
                    break;
                case (-1, 1):
                    AttackPivotPoint = 135;
                    break;
                case (-1, 0):
                    AttackPivotPoint = 180;
                    break;
                case (-1, -1):
                    AttackPivotPoint = 225;
                    break;
                case (0, -1):
                    AttackPivotPoint = 270;
                    break;
                case (1, -1):
                    AttackPivotPoint = 315;
                    break;

            }

        }


        private void Animate()
        {
            _isMoving = _movementDelta.sqrMagnitude > 0.01f;
            Vector2 animDirection = _isMoving ? _movementDelta : _lastMovementDirection;


            _animator.SetFloat("X", animDirection.x);
            _animator.SetFloat("Y", animDirection.y);

            if (_isAttacking)
            {
                _animator.SetBool("IsMoving", false);
                _animator.SetBool("IsAttacking", true);
                _isAttacking = false;
            }
            else
            {
                _animator.SetBool("IsAttacking", false);
                _animator.SetBool("IsMoving", _isMoving);
            }
        }



        //---------------------------------------------------------------------------------------------------//



        public void TakeDamage(Damage damage)
        {
            if (!_isAlive) return;
            if (!_currentlyInvincible) StartCoroutine(DamageRoutine(damage));
        }



        private IEnumerator DamageRoutine(Damage damage)
        {
            _health -= damage.Potency;
            _currentlyInvincible = true;

            bool isRed = false;

            for (int i = 0; i < 8; i++)
            {
                if (isRed) _spriteRenderer.color = Color.white;
                else _spriteRenderer.color = Color.red;

                isRed = !isRed;

                yield return _invincibilityTick;
            }

            if (_health <= 0) _isAlive = false;

            _currentlyInvincible = false;
        }




        private IEnumerator AttackLoop()
        {
            while (_isAlive)
            {
                _isAttacking = true;

                yield return _cooldownWait;
            }
        }


        private void Attack()
        {
            var hits = Physics2D.OverlapCircleAll(transform.position, AttackDistance, _enemyLayer);

            foreach (var target in hits)
            {
                if (Vector2.Angle(Quaternion.Euler(0, 0, AttackPivotPoint) * transform.right, (target.transform.position - transform.position).normalized) < _attackRadius / 2f)
                {
                    target.GetComponent<IDamageable>().TakeDamage(new Damage { Potency = _damage, PushForce = _knockBack });
                }
            }

            _audioSource.Play();
        }



        private void OnCollisionStay2D(Collision2D collision)
        {
            TakeDamage(new Damage { Potency = 1, PushForce = 0 });
        }




        private void Interact()
        {
            if (_interactable == null) return;

            _interactable.Interact();
        }


        private void EnteredInteractRange(Collider2D collision)
        {
            _interactable = collision.gameObject.GetComponent<IInteractable>();
            _interactable.InRange();
        }

        private void ExitedInteractRange(Collider2D collision)
        {
            _interactable.LeftRange();
            _interactable = null;
        }



        public void ModifyDamage(float modifier) => _damage *= modifier;

        public void ModifyHealth(float amount) => _health += amount;

        public void ModifyAttackSpeed(float modifier)
        {
            _cooldown *= modifier;
            Mathf.Clamp(_cooldown, .667f, float.MaxValue);
            _cooldownWait = new(_cooldown);
        }

        public void ModifyAttackConeRadius(float amount) => _attackRadius += amount;

        public void ModifyAttackDistance(float amount) => _attackDistance *= amount;

        public void ModifyKnockback(float modifier)
        {
            var modifyAmount = BaseKnockback * modifier;
            _knockBack += modifyAmount;
        }


    }
}