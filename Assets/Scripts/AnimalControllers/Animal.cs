using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Controls))]
public abstract class Animal : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _prefabDeathParticles;

    public int Experience;
    public AnimalSo AnimalSo;

    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;
    protected SpriteRenderer _spriteRenderer;
    protected AnimalAttackTrigger _attackTrigger;
    protected Animator _animator;

    public UnityEvent OnDeath = new();
    public UnityEvent OnExperience = new();

    protected Controls _controls;

    private SpawnManager _spawnManager;

    protected float _size = 1;

    public bool IsGrounded;

    private float _attackCooldown;


    void Start()
    {
        _size = transform.localScale.x;

        if (_prefabDeathParticles == null)
            _prefabDeathParticles = GlobalDataSo.Instance.PrefabDefaultDeathEffect;

        _spawnManager = SpawnManager.Instance;
        var controlComponents = GetComponents<Controls>();
        _controls = controlComponents.FirstOrDefault(x => x.enabled);
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _attackTrigger = GetComponentInChildren<AnimalAttackTrigger>();
        TryGetComponent<Animator>(out _animator);
        _collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (_attackCooldown > 0)
            _attackCooldown -= Time.deltaTime;
        else if (_controls.Attack)
        {
            _attackCooldown = 1f;
            Attack();
        }

        // Flip if Animal is heading the other way
        if (_rigidbody.velocity.x < -0.1f)
            transform.localScale = Vector3.one * _size;
        else if (_rigidbody.velocity.x > 0.1f)
            transform.localScale = new Vector3(-_size, _size, _size);
    }

    public void SetControls(Controls controls)
    {
        this._controls = controls;
    }

    public void Attack()
    {
        var animal = _attackTrigger.TargetList.FirstOrDefault();
        if (animal != null)
        {
            if (AnimalSo.Level > animal.AnimalSo.Level)
            {
                AddExperience(animal.Experience);

                if (Experience >= AnimalSo.XpUntilLevelup)
                {
                    EffectsManager.Instance.SpawnEffect(
                        GlobalDataSo.Instance.PrefabLevelUpEffect,
                        transform.position);
                    _spawnManager.LevelUp(this);
                }

                LeanTween.move(gameObject, animal.transform.position, 0.5f)
                    .setEaseInOutBounce();

                animal.Kill();
            }
            else
            {
                LeanTween.move(animal.gameObject, transform.position, 0.5f)
                    .setEaseInOutBounce();
                Kill();
            }
        }
    }

    private void AddExperience(int xp)
    {
        Experience += xp;
        OnExperience.Invoke();
    }

    public void Kill()
    {
        OnDeath.Invoke();
        EffectsManager.Instance.SpawnEffect(_prefabDeathParticles ?? GlobalDataSo.Instance.PrefabDefaultDeathEffect, transform.position);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
            IsGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
            IsGrounded = false;
    }
}
